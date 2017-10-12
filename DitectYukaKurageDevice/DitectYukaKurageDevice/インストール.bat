@echo off

copy /Y DitectYukaKurageDevice.exe "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup\" & echo "インストール完了"

ctof "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup\DitectYukaKurageDevice.exe" 


explorer "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup"

timeout 15