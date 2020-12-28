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
set ROM=dash_sgl20.sfc
set PATCH=..\dash_sgl20.bps
set TOTAL=..\..\..\ItemRandomizer\patches

cp -f %VANILLA% %ROM%
echo %ROM% =  %VANILLA%
%BIP% ..\resource\common_rando_patches.ips %ROM%
%BIP% %TOTAL%\dachora.ips %ROM%
%BIP% %TOTAL%\early_super_bridge.ips %ROM%
%BIP% %TOTAL%\g4_skip.ips %ROM%
%BIP% %TOTAL%\high_jump.ips %ROM%
%BIP% %TOTAL%\max_ammo_display.ips %ROM%
%BIP% %TOTAL%\moat.ips %ROM%
%BIP% %TOTAL%\nova_boost_platform.ips %ROM%
%BIP% %TOTAL%\red_tower.ips %ROM%
%BIP% %TOTAL%\spazer.ips %ROM%
%BIP% %TOTAL%\wake_zebes.ips %ROM%
%BIP% %TOTAL%\credits.ips %ROM%
%BIP% %TOTAL%\tracking.ips %ROM%
%BIP% %TOTAL%\introskip_doorflags.ips %ROM%
%BIP% %TOTAL%\seed_display.ips %ROM%
%BIP% ..\resource\dash_SGL2020.ips %ROM%
%BPS% %ROM% %PATCH%

pause
exit 0
