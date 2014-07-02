@echo off
"Source\.nuget\NuGet.exe" update "Source\KickStart.sln" -r "Source\packages"
msbuild master.proj /t:refresh