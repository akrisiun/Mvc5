#@REM bash 
#@REM find . -type -f | grep .nupkg

nuget pack src\System.Web.Razor\Beta.Web.Razor.nuspec                            -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.WebPages\Beta.Web.WebPages.nuspec                      -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.WebPages.Razor\Beta.Web.WebPages.Razor.nuspec -Symbols -Symbols -Build -Version 3.3.1
nuget pack src\System.Web.Mvc\Beta.Web.Mvc.nuspec                                -Symbols -Build -Version 5.5.1
