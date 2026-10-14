@echo off
rem Doble clic para ACTIVAR el jiggle (corre oculto, sin ventana).
cd /d "%~dp0"

if not exist jiggle.exe (
    echo Compilando por unica vez...
    "%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /platform:x64 /target:winexe /optimize /out:jiggle.exe jiggle.cs
    if errorlevel 1 (
        echo Error al compilar.
        pause
        exit /b 1
    )
)

tasklist /fi "imagename eq jiggle.exe" | find /i "jiggle.exe" >nul
if %errorlevel%==0 (
    echo Ya estaba activo.
) else (
    start "" "%~dp0jiggle.exe"
    echo ACTIVADO. Mueve el mouse cada 3 minutos.
)
timeout /t 2 >nul
