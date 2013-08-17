@echo off
if "%~1" == "" goto :Launch
call "C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86_amd64
set ProjectName=%~1
set ProjectRoot=%~dp0
pushd "%ProjectRoot%"
set ProjectRoot=%CD%
title Project '%ProjectName%' (root='%ProjectRoot%')
doskey.exe bd=msbuild.exe /p:Configuration=Debug /t:Build
doskey.exe br=msbuild.exe /p:Configuration=Release /t:Build
doskey.exe bcd=msbuild.exe /p:Configuration=Debug /t:Clean
doskey.exe bcr=msbuild.exe /p:Configuration=Release /t:Clean
doskey.exe xut="%ProjectRoot%\external\xUnit.net\xunit.console.clr4.exe" $*
popd
goto :eof
:Launch
for %%* in (.) do set ProjectName=%%~n*
cmd.exe /k %0 %ProjectName%
