@echo off
rem Doble clic para DESACTIVAR el jiggle.
taskkill /f /im jiggle.exe >nul 2>&1
if %errorlevel%==0 (echo DESACTIVADO.) else (echo No estaba activo.)
timeout /t 2 >nul
