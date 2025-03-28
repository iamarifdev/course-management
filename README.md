## To add a migration

#### Change the directory to the infrastructure project
```bash
cd src/CourseManagement.Infrastructure
```

#### Add a migration
```bash
dotnet ef migrations add <MigrationName> --startup-project ../CourseManagement.API -o ./Database/Migrations/ --context ApplicationDbContext
```

#### Update the database
```bash
dotnet ef database update --startup-project ../CourseManagement.API --context ApplicationDbContext
```

## To run the project using Docker compose

#### Important: Follow this official reference to set up https secret and certificate in different environments (Linux, Windows, Mac)
https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-9.0

This project is using a self-signed certificate in Mac and linux containers. Specifically this section is followed:
https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-9.0

### To run the solution using Docker compose with overrides
- Change the directory to the root of the project
- Run the following command
    ```bash
    docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```

### To stop the containers
- Run the following command
    ```bash
      docker compose -f docker-compose.yml -f docker-compose.override.yml down
    ```

## To see the structure logs and traces of the services
Visit the following link address in your browser
http://localhost:8082

## To run the tests
- ### Change the directory to root directory
  ```bash
  dotnet test
  ```

**Important:** For functional tests **Docker** must be **installed and running**.
It uses TestContainers to run the tests which requires Docker to be running.
Ref: https://testcontainers.com/

