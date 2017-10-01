for /f "usebackq" %%i in (`dir /B /S *.wav`) do (
    ffmpeg -i %%i %%i.mp3
)

pause