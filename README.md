# covid19
Web application developed on ASP.NET core 3.1 technology containing online form to report individual health during COVID-19 situation.

## Tools
### Linux
* Ubuntu 18.04.3 (LTS) x64 (We tested on this version)
* Docker
* Docker Compose
### Windows Server
* Visual Studio 2019 Community Edition or Visual Studio Code (or any which can build .NET core)
* SQL Server 2017 Express Edition
* Azure Data Studio or SQL Server Management Studio (or any which can be manipulate data)

## Build & Deploy
### Linux
1. Clone repository into your prefered directory
1. Adjust volume setting for SQL Server Express installation to your desire
1. Simply run `docker-compose build` then `docker-compose up -d`
1. Connect to database and run SQL scripts we provided in order
   1. **create_database.sql** -> Database name **[covid19_db]** will be created in your SQL Server
   1. **create_table.sql** -> Tables and Stored Procedures will be created in your **[covid19_db]**
   1. **lut_data.sql** -> Necessary lookup data will be inserted into tables
1. Adjust connection string to point to your database server by editing **Covid19\appsettings.Staging.json**
   * Or if you want this to be Production, simply change `ENV ASPNETCORE_ENVIRONMENT "Staging"` in ****App.Dockerfile**** to be `ENV ASPNETCORE_ENVIRONMENT "Production"` then create a file name **Covid19\appsettings.Production.json** to contain your connection string
### Windows Server
1. Clone repository into your prefered directory
1. Run SQL scripts we provided in order
   1. **create_database.sql** -> Database name **[covid19_db]** will be created in your SQL Server
   1. **create_table.sql** -> Tables and Stored Procedures will be created in your **[covid19_db]**
   1. **lut_data.sql** -> Necessary lookup data will be inserted into tables
1. Adjust connection string to point to your database server by editing **Covid19\appsettings.json** (Or any environment settings you desire)
1. Publish web application using your prefered tools (We recommend Visual Studio 2019 Community Edition latest version for your convenient)
1. Copy published application to your designated web server
