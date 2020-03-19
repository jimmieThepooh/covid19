# covid19
Web application developed on ASP.NET core 3.1 technology containing online form to report individual health during COVID-19 situation.

## Tools
* Visual Studio 2019 Community Edition or Visual Studio Code (or any which can build .NET core)
* SQL Server 2017 Express Edition
* Azure Data Studio or SQL Server Management Studio (or any which can be manipulate data)

## Build & Deploy
1. Clone repository into your prefered directory
1. Run SQL scripts we provided in order
   1. **create_database.sql** -> Database name **[covid19_db]** will be created in your SQL Server
   1. **create_table.sql** -> Tables and Stored Procedures will be created in your **[covid19_db]**
   1. **lut_data.sql** -> Necessary lookup data will be inserted into tables
1. Adjust connection string to point to your database server by editing **Covid19\appsettings.json** (Or any environment settings you desire)
1. Publish web application using your prefered tools (We recommend Visual Studio 2019 Community Edition latest version for your convenient)
1. Copy published application to your designated web server
