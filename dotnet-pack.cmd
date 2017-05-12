@REM bash 
@REM find . -type -f | grep .nupkg

dotnet restore

dotnet build src\System.Web.Razor\System.Web.Razor.csproj
dotnet build src\System.Web.WebPages\System.Web.WebPages.csproj 
dotnet build src\System.Web.WebPages.Razor\System.Web.WebPages.Razor.csproj
dotnet build src\System.Web.Mvc\System.Web.Mvc.csproj

nuget pack src\System.Web.Razor\System.Web.Razor.csproj
nuget pack src\System.Web.WebPages\System.Web.WebPages.csproj 
nuget pack src\System.Web.WebPages.Razor\System.Web.WebPages.Razor.csproj
nuget pack src\System.Web.Mvc\System.Web.Mvc.csproj

@PAUSE