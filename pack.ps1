
dotnet sln list
# remove-item *.nupkg -force

dotnet pack src\System.Net.Http.Formatting\System.Net.Http.Formatting.csproj  -o $PWD
dotnet pack src\System.Web.Http\System.Web.Http.csproj                        -o $PWD
dotnet pack src\System.Web.Http.WebHost\System.Web.Http.WebHost.csproj        -o $PWD

dotnet build src\System.Net.Http.Formatting\System.Net.Http.Formatting.csproj  -o $PWD/lib/ -f net471
dotnet build src\System.Web.Http.WebHost\System.Web.Http.WebHost.csproj        -o $PWD/lib/ -f net471
dotnet build src\System.Web.Http\System.Web.Http.csproj                        -o $PWD/lib/ -f net471

exit;

nuget pack src\System.Web.Mvc\System.Web.Mvc.csproj

nuget pack src\System.Web.Helpers\System.Web.Helpers.csproj
nuget pack src\System.Web.Razor\System.Web.Razor.csproj
nuget pack src\System.Web.WebPages\System.Web.WebPages.csproj
nuget pack src\System.Web.WebPages.Deployment\System.Web.WebPages.Deployment.csproj
nuget pack src\System.Web.WebPages.Razor\System.Web.WebPages.Razor.csproj

nuget pack src\System.Web.Http.Cors\System.Web.Http.Cors.csproj
nuget pack src\System.Web.Cors\System.Web.Cors.csproj
