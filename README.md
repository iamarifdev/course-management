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

### To start the services with overrides
- Change the directory to the root of the project
- Run the following command
    ```bash
    docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```

### To stop the services
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

## To check the Swagger API documentation
Visit the following link address in your browser
http://localhost:8080/swagger

