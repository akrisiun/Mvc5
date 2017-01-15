nuget install Microsoft.Web.Infrastructure   -outputDirectory Packages
nuget install Newtonsoft.Json -version 9.0.1 -outputDirectory Packages

copy packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll lib\*.* 
copy packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll  lib\*.* 