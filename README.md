# Silly Cat Trader Icons

Server mod for SPT `4.0.x` that replaces trader avatars with the cat-themed icons.

Originally forked from `jbs4bmx/AlternativeTraderPics`, now independently maintained as `Silly Cat Trader Icons`.

## Requirements

- SPT `4.0.x`
- SPT `4.0.13` is the current target for this branch
- .NET `9` runtime to run SPT
- .NET `9` SDK if you want to build this repo yourself

## Build

```bash
dotnet build mod/SillyCatTraderIcons.csproj -c Release
```

## Install

1. Build the project in `Release` mode.
2. Copy the folder inside `mod/bin/Release/` into your SPT `mods/` folder.
3. Edit `config.jsonc` if you want to limit which trader avatars are replaced.
4. Start SPT. The mod copies the selected avatar files into `sptappdata/files/trader/avatar/` on startup.

## Configuration

The mod reads [`mod/config.jsonc`](mod/config.jsonc). When `updateAllTraders` is `true`, all supported trader avatars included in `res/` are copied into the SPT avatar cache. When it is `false`, only the traders enabled in the config are updated.

The `extension` setting controls which files are read from the installed mods `res/` folder. The loader writes both `.jpg` and `.png` cache variants so mixed trader avatar requests in SPT `4.0.x` still resolve cleanly.

## Notes

- The included `Rename-Cached_Images.bat` script is optional and can still be used to manually rename cached avatar files.

## License

Distributed under the MIT License. See [LICENSE.txt](LICENSE.txt).
