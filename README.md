## To add a migration

#### Change the directory to the infrastructure project
```bash
cd src/CourseManagement.Infrastructure
```

#### Add a migration
```bash
dotnet ef migrations add Initialize --startup-project ../CourseManagement.API -o ./Database/Migrations/ --context ApplicationDbContext
```

#### Update the database
```bash
dotnet ef database update --startup-project ../CourseManagement.API --context ApplicationDbContext
```