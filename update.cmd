@echo off
NuGet.exe update "Source\KickStart.sln" -r "Source\packages"
msbuild master.proj /t:refresh