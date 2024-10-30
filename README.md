# CounterApp
## Purpose
The purpose of the Counter App is to process scenario-based data and generate reports efficiently. It aims to provide users with insights from smart meter data, facilitating better decision-making and operational efficiency in energy management. The app leverages microservices architecture to ensure scalability and responsiveness, allowing for seamless data handling and reporting.

The Counter App accommodates various scenarios, including erroneous commits, merges, and layout inconsistencies in graphs. It supports multiple architectures, allowing simultaneous use of PostgreSQL and MSSQL. Developers can tailor the app according to their needs, integrating with .NET 8.0, .NET Standard 2.0, and .NET Framework 4.8. This flexibility enables the integration of both legacy and modern projects, whether they are dependent or independent.

The Counter App features a reporting structure supported by RabbitMQ messaging queues. This integration enhances the application’s ability to process and manage reports asynchronously, improving overall efficiency and reliability.

## Technologies Used (Optional)

    .NET Core 8.0: The main framework for building the application with modern features.
    .NET Standard 2.0: Provides a set of APIs that can be used across different .NET implementations.
    .NET Framework 4.8: Supports legacy applications and enables integration with older projects.
    MsSQL: A relational database management system for data storage and retrieval.
    PostgreSQL: An alternative relational database providing additional features and flexibility.
    RabbitMQ: A messaging queue that supports asynchronous report generation.
    Docker: Enables containerization for consistent deployments across various environments.

### Running the Project (Optional)
1. **Clone the Project**:
    ```bash
    git clone https://github.com/unlubaha/CounterApp.git
    ```

2. **Configure RabbitMQ**:
    Ensure that a database and RabbitMQ instances are running before starting the project. If using Docker, run:
    ```bash
    docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    ```
    Add your RabbitMQ connection details to the appsettings.json:
   ```json
   "RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest",
    "VirtualHost": "/"
  }

3. **Configure Database Connection (Optional)**:
    - **For MSSQL**:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=DatabaseName;User Id=User;Password=Password;TrustServerCertificate=True;"
    }
    ```
    - **For PostgreSQL**:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "User ID=User;Password=Password;Server=localhost;Port=5432;Database=DatabaseName;IntegratedSecurity=true;Pooling=True;"
    }
    ```

4. **Run Migrations** (Optional):
    Apply migrations for the databases with same or different sql technolgy or database with same or different sql technolgy :
    (For PostgreSQL you can find to Postgre branch)
    You can use this with changing your appsettings files.
    ```bash
    dotnet ef database update --project Counter.CountService
    dotnet ef database update --project Counter.ReportService
    ```
    ----------------------------------------------------------------
    Directly you can work with a database and table with main branch.

   ```bash
    dotnet ef database update --project Counter.Entities
    ```

5. **Run the Project**:
    Also you can use IIS express or Docker.
    ```bash
    dotnet run --project Counter.CountService
    dotnet run --project Counter.ReportService
    dotnet run --project Counter.Web.UI
    ```

7. **WebAPI Configuration**:
    Update and modify the `launchSettings.json` for Swagger access at:
    ```url
    http://localhost:{port}/swagger
    ```

 8. **Web.UI Configuration**:
    Update and modify addresses in Javascripts.
```bash
const response = await fetch(`https://localhost:5179/api/Report/Reports?serialNumber=${serialNumber}`);
```
