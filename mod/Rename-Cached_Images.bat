@echo off
setlocal enabledelayedexpansion
REM Cached Trader Image Cleanup Script
:: ====================================
::       author | jbs4bmx
:: created date | [DMY] 13.07.2024
::  modify date | [DMY] 13.07.2024
:: ====================================

:menu
cls
echo.=============================================
echo.     Cached Trader Image Renaming Script
echo.=============================================
echo.  1. Rename files
echo.  2. Restore original trader icons
echo.  3. Cancel ^(Exit^)
echo.=============================================
set /p choice="Enter your choice (1, 2, or 3): "

if "%choice%"=="1" goto rename_files
if "%choice%"=="2" goto revert_files
if "%choice%"=="3" goto end
echo Invalid choice, please try again.
pause
goto menu

:rename_files
cls && echo.
echo. Renaming cached trader images...
for %%f in (..\..\sptappdata\files\trader\avatar\*.png) do (
    ren "%%f" "%%~nf.png.bak"
)
for %%g in (..\..\sptappdata\files\trader\avatar\*.jpg) do (
    ren "%%g" "%%~ng.jpg.bak"
)
goto menu

:revert_files
cls && echo.
echo. Restoring cached trader images...
for %%h in (..\..\sptappdata\files\trader\avatar\*.*) do (
    if /I "%%~xh" NEQ ".bak" (
        del "%%h"
    )
)
for %%p in (..\..\sptappdata\files\trader\avatar\*.png.bak) do (
    ren "%%p" "%%~np"
)
for %%j in (..\..\sptappdata\files\trader\avatar\*.jpg.bak) do (
    ren "%%j" "%%~nj"
)
goto menu

:end
echo.
echo.Exiting script...
exit 0
