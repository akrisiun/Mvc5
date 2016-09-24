
cd src\System.Web.Razor\
dotnet restore
dotnet build
cd ..\System.Web.WebPages\
dotnet restore
dotnet build

cd ..\System.Web.WebPages.Razor\
dotnet restore
dotnet build

cd ..\System.Web.Mvc\
dotnet restore
dotnet build

cd ..\..\..\

@PAUSE