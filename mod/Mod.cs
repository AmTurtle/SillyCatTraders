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
    private static readonly Dictionary<string, string[]> TraderFiles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["updatePrapor"] = ["59b91ca086f77469a81232e4"],
        ["updateTherapist"] = ["59b91cab86f77469aa5343ca"],
        ["updateFence"] = ["579dc571d53a0658a154fbec"],
        ["updateSkier"] = ["59b91cb486f77469a81232e5"],
        ["updatePeacekeeper"] = ["59b91cbd86f77469aa5343cb"],
        ["updateMechanic"] = ["5a7c2ebb86f7746e324a06ab"],
        ["updateRagman"] = ["5ac3b86a86f77461491d1ad8"],
        ["updateJaeger"] = ["5c06531a86f7746319710e1b"],
        ["updateLightKeeper"] = ["638f541a29ffd1183d187f57"],
        ["updateBTRDriver"] = ["656f0f98d80a697f855d34b1"],
        ["updateRef"] = ["6617beeaa9cfa777ca915b7c"],
        ["Scorpion"] = ["6688d464bc40c867f60e7d7e"],
        ["AIOTrader"] = ["aiotrader", "blueheadtrader"],
        ["AKGuy"] = ["AKGUY"],
        ["AnastasiaSvetlana"] = ["Anastasia", "Svetlana"],
        ["ARSHoppe"] = ["armalite"],
        ["Bootlegger"] = ["bootlegger"],
        ["DRIP"] = ["moron"],
        ["GearGal"] = ["GearGal"],
        ["GoblinKing"] = ["GoblinKing"],
        ["Gunsmith"] = ["gunsmith"],
        ["IProject"] = ["PRTS"],
        ["KatarinaBlack"] = ["kat", "sepi1"],
        ["KeyMaster"] = ["keymaster"],
        ["MFACShop"] = ["MFAC"],
        ["Priscilu"] = ["Priscilu"],
        ["Questor"] = ["questor"],
        ["TheBroker"] = ["broker_portrait1"],
        ["cuteTrader"] = ["kwmKYUUTO"],
        ["zeroTrader"] = ["kwmZERO"],
        ["Legs"] = ["Legs"],
        ["sashahimik"] = ["himik"],
        ["ArtemTrader"] = ["66bf757f27d0b097db0acea5"]
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

        HashSet<string> selectedNames = GetSelectedTraderNames(config, sourceDirectory);
        int copiedCount = 0;

        foreach (string traderName in selectedNames)
        {
            string sourcePath = Path.Combine(sourceDirectory, $"{traderName}.{config.Extension}");
            if (!File.Exists(sourcePath))
            {
                logger.Warning($"Silly Cat Trader Icons: Missing source image for '{traderName}' at {sourcePath}");
                continue;
            }

            copiedCount += CopyAvatarVariants(sourcePath, avatarDirectory, traderName);
        }

        logger.Info($"Silly Cat Trader Icons v4.0.0: Updated {selectedNames.Count} trader image(s), wrote {copiedCount} file(s) to cache.");
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

    private static HashSet<string> GetSelectedTraderNames(TraderAvatarConfig config, string sourceDirectory)
    {
        if (config.UpdateAllTraders)
        {
            return Directory
                .EnumerateFiles(sourceDirectory, $"*.{config.Extension}", SearchOption.TopDirectoryOnly)
                .Select(file => Path.GetFileNameWithoutExtension(file))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        HashSet<string> selected = new(StringComparer.OrdinalIgnoreCase);
        foreach ((string configKey, string[] traderNames) in TraderFiles)
        {
            if (config.IsEnabled(configKey))
            {
                foreach (string traderName in traderNames)
                {
                    selected.Add(traderName);
                }
            }
        }

        return selected;
    }

    private static int CopyAvatarVariants(string sourcePath, string avatarDirectory, string traderName)
    {
        int copyCount = 0;
        foreach (string extension in new[] { "jpg", "png" })
        {
            string destinationPath = Path.Combine(avatarDirectory, $"{traderName}.{extension}");
            File.Copy(sourcePath, destinationPath, overwrite: true);
            copyCount++;
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
