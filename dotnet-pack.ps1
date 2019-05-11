#@REM bash 
#@REM find . -type -f | grep .nupkg

dotnet restore

dotnet build src\System.Web.Razor\System.Web.Razor.csproj
dotnet build src\System.Web.WebPages\System.Web.WebPages.csproj 
dotnet build src\System.Web.WebPages.Razor\System.Web.WebPages.Razor.csproj
dotnet build src\System.Web.Mvc\System.Web.Mvc.csproj

nuget pack src\System.Web.Razor\Beta.Web.Razor.nuspec                            -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.WebPages\Beta.Web.WebPages.nuspec                      -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.WebPages.Razor\Beta.Web.WebPages.Razor.nuspec -Symbols -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.Mvc\Beta.Web.Mvc.nuspec                                -Symbols -Build -Version 5.5.1

# @PAUSE