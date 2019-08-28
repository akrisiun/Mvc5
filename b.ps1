
dotnet restore

.\build.cmd EnableSkipStrongNames

# %MSBuild% 
msbuild16 Runtime.msbuild /m /nr:false /p:Platform="Any CPU" /p:Desktop=true /v:M /fl 

# dotnet build -o $PWD\lib\ --no-restore -f net461
dotnet build -o $PWD\lib\ --no-restore   -f net471 
