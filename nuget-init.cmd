nuget install Microsoft.Web.Infrastructure   -outputDirectory Packages
nuget install Newtonsoft.Json -version 9.0.1 -outputDirectory Packages

nuget install Microsoft.Owin -version 2.0.2  -outputDirectory Packages
nuget install Owin           -version 1.0    -outputDirectory Packages

mkdir ..\lib

copy packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll ..\lib\*.*
copy packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll lib\*.* 
copy packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll  lib\*.* 
copy packages\Owin.1.0\lib\net40\Owin.dll                          lib\*.* 
copy packages\Microsoft.Owin.2.0.2\lib\net45\Microsoft.Owin.dll    lib\*.* 