@echo off

set VANILLA=..\resource\SuperMetroid.sfc

:: **********

if not exist %VANILLA% (
   echo Missing vanilla rom: %VANILLA%
   pause
   exit 1
)

for /f %%i in ('..\tools\bip.exe --crc32 %VANILLA%') do (
   if not "%%i" == "d63ed5f8" (
      echo Invalid vanilla rom: %VANILLA%
      pause
      exit 2
   )
)

:: **********

set BIP=..\tools\bip.exe -n %VANILLA%
set BPS=..\tools\flips.exe --create --bps %VANILLA%
set TOTAL=..\..\..\ItemRandomizer\patches

call :build_hack dash_v10 savestates ..\resource\dash_v10.ips
call :build_hack dash_v10 no_savestates ..\resource\dash_v10.ips

call :build_hack dash_SGL20 savestates ..\resource\dash_SGL2020.ips
call :build_hack dash_SGL20 no_savestates ..\resource\dash_SGL2020.ips

pause
exit 0

:: **********

:build_hack
echo --- %1 %2 ---
set HACK=%1_hack_%2
..\tools\bip.exe ..\resource\smhack21_b3_%2.bps %VANILLA% -o %HACK%.sfc
call :patch_hack %HACK%.sfc
%BIP% %3 %HACK%.sfc
%BPS% %HACK%.sfc ..\%HACK%.bps
echo.
exit /b 0

:: **********

:patch_hack
%BIP% ..\resource\common_rando_patches.ips %1
%BIP% %TOTAL%\dachora.ips %1
%BIP% ..\resource\door_mods.ips %1
%BIP% %TOTAL%\early_super_bridge.ips %1
%BIP% %TOTAL%\g4_skip.ips %1
%BIP% %TOTAL%\high_jump.ips %1
%BIP% %TOTAL%\max_ammo_display.ips %1
%BIP% %TOTAL%\moat.ips %1
%BIP% %TOTAL%\nova_boost_platform.ips %1
%BIP% %TOTAL%\red_tower.ips %1
%BIP% %TOTAL%\spazer.ips %1
%BIP% %TOTAL%\wake_zebes.ips %1
exit /b 0
