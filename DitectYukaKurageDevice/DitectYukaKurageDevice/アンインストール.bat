@echo off

taskkill /im DitectYukaKurageDevice.exe /F /T 

del "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup\DitectYukaKurageDevice.exe" & echo "�A���C���X�g�[������" 

explorer "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup"

timeout 15