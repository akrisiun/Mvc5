
nuget restore RuntimePortable.sln

nuget restore test\System.Web.Razor.Test\System.Web.Razor.Test.csproj

nuget install Microsoft.Web.Infrastructure   -outputDirectory Packages
nuget install Newtonsoft.Json -version 9.0.1 -outputDirectory Packages
nuget install Newtonsoft.Json -version 12.0.1 -outputDirectory Packages

nuget install Microsoft.Owin -version 2.0.2  -outputDirectory Packages
nuget install Owin           -version 1.0    -outputDirectory Packages

nuget install Moq            -version 4.10.1 -outputDirectory Packages
nuget install xunit.core     -version 2.4.1  -outputDirectory Packages
nuget install xunit          -Version 2.4.1  -outputDirectory Packages
nuget install xunit.runner.visualstudio -Version 2.4.1  -outputDirectory Packages
nuget install Microsoft.NET.Test.Sdk    -Version 15.3.0-preview-20170427-09  -outputDirectory Packages

mkdir ..\lib

copy packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll ..\lib\*.*
copy packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll lib\*.* 
@REM copy packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll  lib\*.* 
copy packages\Newtonsoft.Json.12.0.1\lib\net45\Newtonsoft.Json.dll  lib\*.* 
copy packages\Owin.1.0\lib\net40\Owin.dll                          lib\*.* 
copy packages\Microsoft.Owin.2.0.2\lib\net45\Microsoft.Owin.dll    lib\*.* 