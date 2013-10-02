IF "%BUILD_NUMBER%"=="" (
SET BUILD_NUMBER=0
)
echo %~dp0BuildUtility.exe /rf %1 /v %BUILD_NUMBER% /ct .cshtml,.aspx,.Master,.ascx /tt .js,.css /sf 
%~dp0BuildUtility.exe /rf %1 /v %BUILD_NUMBER% /ct .cshtml,.aspx,.Master,.ascx /tt .js,.css /sf 

EXIT /B %errorlevel%