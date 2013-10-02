StaticResourceVersioningTool
============================

A .net based command line utility to parse any  static files like .cshtml, html, css in your project 
and update/add the build version number in the static resource url.

This tool can be used prior to bundle your artifact for final deployment for production. The advantage is,it is not require to 
checkin those files in your repo. It just append a version number to all the static urls mentioned in your source code.

# Before
`````
http://yourdomain.com/script/app.js
`````
# After
`````
http://yourdomain.com/script/app.js?v=1.2.3
`````

# Usage
`````
BuildUtility.exe /rf %1 /v %BUILD_NUMBER% /ct .cshtml,.aspx,.Master,.ascx /tt .js,.css /sf 

BuildUtility.exe /rf d:/codebase/ /v 1.2.3 /ct .cshtml,.aspx,.Master,.ascx /tt .js,.css /sf 
`````

# Switches

/sf - Scan sub folders

/m - Minificatin required - yet to build

/vo - Verbose output

/u - Update version

/? - help

/rf - Root folder

/v - Version number

/ct - Content file types

/tt - Target file types

