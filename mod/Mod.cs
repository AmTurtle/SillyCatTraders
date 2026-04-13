using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Range = SemanticVersioning.Range;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;

namespace SillyCatTraderIcons;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.amturtle.sillycattradericons";
    public override string Name { get; init; } = "Silly Cat Trader Icons";
    public override string Author { get; init; } = "AmTurtle";
    public override List<string>? Contributors { get; init; } = ["jbs4bmx", "Revingly"];
    public override SemanticVersioning.Version Version { get; init; } = new("4.0.0");
    public override Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, Range>? ModDependencies { get; init; }
    public override string? Url { get; init; } = null;
    public override bool? IsBundleMod { get; init; } = false;
    public override string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class TraderAvatarLoader(ISptLogger<TraderAvatarLoader> logger) : IOnLoad
{
    private sealed record TraderImageMapping(string SourceName, params string[] DestinationNames);

    private static readonly Dictionary<string, TraderImageMapping[]> TraderFiles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["updatePrapor"] = [new("59b91ca086f77469a81232e4", "59b91ca086f77469a81232e4")],
        ["updateTherapist"] = [new("59b91cab86f77469aa5343ca", "59b91cab86f77469aa5343ca")],
        ["updateFence"] = [new("579dc571d53a0658a154fbec", "579dc571d53a0658a154fbec")],
        ["updateSkier"] = [new("59b91cb486f77469a81232e5", "59b91cb486f77469a81232e5")],
        ["updatePeacekeeper"] = [new("59b91cbd86f77469aa5343cb", "59b91cbd86f77469aa5343cb")],
        ["updateMechanic"] = [new("5a7c2ebb86f7746e324a06ab", "5a7c2ebb86f7746e324a06ab")],
        ["updateRagman"] = [new("5ac3b86a86f77461491d1ad8", "5ac3b86a86f77461491d1ad8")],
        ["updateJaeger"] = [new("5c06531a86f7746319710e1b", "5c06531a86f7746319710e1b")],
        ["updateLightKeeper"] = [new("638f541a29ffd1183d187f57", "638f541a29ffd1183d187f57")],
        ["updateBTRDriver"] = [new("656f0f98d80a697f855d34b1", "656f0f98d80a697f855d34b1")],
        ["updateRef"] = [new("6617beeaa9cfa777ca915b7c", "6617beeaa9cfa777ca915b7c")],
        ["Scorpion"] = [new("scorpion", "6688d464bc40c867f60e7d7e", "scorpion")],
        ["AIOTrader"] = [new("aiotrader", "aiotrader"), new("bluehead", "blueheadtrader", "bluehead")],
        ["AKGuy"] = [new("AKGUY", "AKGUY")],
        ["AnastasiaSvetlana"] = [new("Anastasia", "Anastasia"), new("Svetlana", "Svetlana")],
        ["ARSHoppe"] = [new("armalite", "armalite")],
        ["Bootlegger"] = [new("bootlegger", "bootlegger")],
        ["DRIP"] = [new("moron", "moron")],
        ["GearGal"] = [new("GearGal", "GearGal")],
        ["GoblinKing"] = [new("GoblinKing", "GoblinKing")],
        ["Gunsmith"] = [new("gunsmith", "gunsmith")],
        ["IProject"] = [new("PRTS", "PRTS")],
        ["KatarinaBlack"] = [new("kat", "kat"), new("sepi1", "sepi1")],
        ["KeyMaster"] = [new("keymaster", "keymaster")],
        ["MFACShop"] = [new("MFAC", "MFAC")],
        ["Priscilu"] = [new("Priscilu", "Priscilu")],
        ["Questor"] = [new("questor", "questor")],
        ["TheBroker"] = [new("broker_portrait1", "broker_portrait1")],
        ["cuteTrader"] = [new("kwmKYUUTO", "kwmKYUUTO")],
        ["zeroTrader"] = [new("kwmZERO", "kwmZERO")],
        ["Legs"] = [new("Legs", "Legs")],
        ["sashahimik"] = [new("himik", "himik")],
        ["ArtemTrader"] = [new("Artem", "66bf757f27d0b097db0acea5", "Artem")]
    };

    public Task OnLoad()
    {
        string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? throw new InvalidOperationException("Unable to resolve mod directory.");
        string sourceDirectory = Path.Combine(modDirectory, "res");
        string configPath = Path.Combine(modDirectory, "config.jsonc");
        string avatarDirectory = Path.GetFullPath(Path.Combine(modDirectory, "..", "..", "sptappdata", "files", "trader", "avatar"));

        if (!Directory.Exists(sourceDirectory))
        {
            logger.Error($"Silly Cat Trader Icons: Source directory not found: {sourceDirectory}");
            return Task.CompletedTask;
        }

        if (!File.Exists(configPath))
        {
            logger.Error($"Silly Cat Trader Icons: Config file not found: {configPath}");
            return Task.CompletedTask;
        }

        TraderAvatarConfig config;
        try
        {
            config = LoadConfig(configPath);
        }
        catch (Exception ex)
        {
            logger.Error($"Silly Cat Trader Icons: Failed to read config.jsonc: {ex.Message}");
            return Task.CompletedTask;
        }

        Directory.CreateDirectory(avatarDirectory);

        List<TraderImageMapping> selectedMappings = GetSelectedMappings(config, sourceDirectory);
        int copiedCount = 0;

        foreach (TraderImageMapping mapping in selectedMappings)
        {
            string sourcePath = Path.Combine(sourceDirectory, $"{mapping.SourceName}.{config.Extension}");
            if (!File.Exists(sourcePath))
            {
                logger.Warning($"Silly Cat Trader Icons: Missing source image for '{mapping.SourceName}' at {sourcePath}");
                continue;
            }

            copiedCount += CopyAvatarVariants(sourcePath, avatarDirectory, mapping.DestinationNames);
        }

        logger.Info($"Silly Cat Trader Icons v4.0.0: Updated {selectedMappings.Count} trader mapping(s), wrote {copiedCount} file(s) to cache.");
        return Task.CompletedTask;
    }

    private static TraderAvatarConfig LoadConfig(string configPath)
    {
        string json = File.ReadAllText(configPath);
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        TraderAvatarConfig? config = JsonSerializer.Deserialize<TraderAvatarConfig>(json, options);
        return config ?? new TraderAvatarConfig();
    }

    private static List<TraderImageMapping> GetSelectedMappings(TraderAvatarConfig config, string sourceDirectory)
    {
        if (config.UpdateAllTraders)
        {
            List<TraderImageMapping> mappings = TraderFiles.Values
                .SelectMany(value => value)
                .DistinctBy(mapping => mapping.SourceName)
                .ToList();

            HashSet<string> knownSources = mappings
                .Select(mapping => mapping.SourceName)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (string looseFile in Directory.EnumerateFiles(sourceDirectory, $"*.{config.Extension}", SearchOption.TopDirectoryOnly))
            {
                string sourceName = Path.GetFileNameWithoutExtension(looseFile);
                if (!knownSources.Contains(sourceName))
                {
                    mappings.Add(new TraderImageMapping(sourceName, sourceName));
                }
            }

            return mappings;
        }

        List<TraderImageMapping> selected = [];
        HashSet<string> seenSources = new(StringComparer.OrdinalIgnoreCase);
        foreach ((string configKey, TraderImageMapping[] mappings) in TraderFiles)
        {
            if (config.IsEnabled(configKey))
            {
                foreach (TraderImageMapping mapping in mappings)
                {
                    if (seenSources.Add(mapping.SourceName))
                    {
                        selected.Add(mapping);
                    }
                }
            }
        }

        return selected;
    }

    private static int CopyAvatarVariants(string sourcePath, string avatarDirectory, IEnumerable<string> traderNames)
    {
        int copyCount = 0;
        foreach (string traderName in traderNames)
        {
            foreach (string extension in new[] { "jpg", "png" })
            {
                string destinationPath = Path.Combine(avatarDirectory, $"{traderName}.{extension}");
                File.Copy(sourcePath, destinationPath, overwrite: true);
                copyCount++;
            }
        }

        return copyCount;
    }
}

public class TraderAvatarConfig
{
    [JsonPropertyName("extension")]
    public string Extension { get; set; } = "jpg";

    [JsonPropertyName("updateAllTraders")]
    public bool UpdateAllTraders { get; set; } = true;

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalSettings { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public bool IsEnabled(string key)
    {
        if (!AdditionalSettings.TryGetValue(key, out JsonElement element))
        {
            return false;
        }

        return element.ValueKind == JsonValueKind.True;
    }
}
