MyApp.Console
      │
      ▼
 MyApp.Data
      │
      ▼
MyApp.Domain

1. appsettings.json
// Make appsettings.json copy to the output directory
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyDb;User Id=sa;Password=your-password;TrustServerCertificate=True"
  }
}

2. configuration packages 
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json

3. EF Core SQL Server:
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef


4. Because DbContext is in MyApp.Data but executable is MyApp.Console, 
I'd use an IDesignTimeDbContextFactory<AppDbContext> in the Data project

5. Add EF Core Tools to the Console project:
dotnet ef migrations add InitialCreate --project EntityFrameworkCore.Data --startup-project EntityFrameworkCore.ConsoleApp
dotnet ef database update --project EntityFrameworkCore.Data --startup-project EntityFrameworkCore.ConsoleApp

!! For a disposable development database, first update the database to the previous migration, and then remove the migration from the project. Use 0 as the target when removing the first migration.
dotnet ef database update [PreviousMigrationName] --project EntityFrameworkCore.Data --startup-project EntityFrameworkCore.ConsoleApp
dotnet ef migrations remove --project EntityFrameworkCore.Data --startup-project EntityFrameworkCore.ConsoleApp


If you need to update the ef tools use:
dotnet tool update --global dotnet-ef

notes:
Use top-level statements.
Make the operation asynchronous all the way through (PrintAllTeamsAsync).

next:
IConsoleApp/application service that represents the actual program

then the separation is
Program.cs
    │
    │ composition / DI
    ▼
ConsoleApp
    │
    │ application logic
    ▼
TeamReader
    │
    │ data access
    ▼
AppDbContext
    │
    ▼
Database


6. To suppress the ef core logging in production, next configuartion level is needed in the appsettings.json file:
{
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
}

7. The EF Core Power Tools is an Visual Studio extension is installable from the Visual Studio Marketplace. 
It provides a graphical interface to manage EF Core migrations, reverse engineer a database, and generate code for your DbContext and entity classes. You can use it to visualize your database schema and generate code based on your existing database.




