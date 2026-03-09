# Stamply - Digital Loyalty System

Stamply is a modern digital loyalty card solution designed to replace physical stamp cards. It empowers businesses to create and manage digital loyalty programs that integrate seamlessly with Apple Wallet and Google Wallet. Customers can earn stamps and redeem rewards simply by scanning a QR code on their digital cards.

Built with a commitment to high engineering standards, Stamply follows **Domain-Driven Design (DDD)** and **Clean Architecture** principles to ensure a scalable and maintainable backend.

## Key Features

- **Digital Wallet Integration:** Seamless support for Apple and Google Wallets.
- **QR Code Stamping:** Fast and secure "zero-friction" stamping via QR scanning.
- **Flexible Program Management:** Businesses can easily define their own loyalty rules and rewards.
- **Advanced Identity & Access:** Granular permissions and secure authentication using JWT.
- **Clean Architecture:** Strictly organized into Domain, Application, Infrastructure, and Presentation layers.
- **Reliable Persistence:** Built on PostgreSQL with EF Core, implementing Repository and Unit of Work patterns.
- **Developer-Friendly Tools:** Automated migrations, Makefile for common tasks, and pre-commit hooks.

  The template includes a query extension that simplifies pagination in your queries.

## Getting Started

1. **Spin up the Database:**
   The project includes a `docker-compose.dev.yml` file to quickly set up a PostgreSQL instance for development.
   ```bash
   make dev-db-start
   ```
   This will start a PostgreSQL container with the default credentials (`postgres`/`postgres`) and database name (`stamply`) as configured in `appsettings.Development.json`.

2. **Install Dependencies:**

   - For Husky (pre-commit hooks), run:
     ```bash
     npx husky-init && npm install
     ```
   - Restore your .NET packages:
     ```bash
     dotnet restore
     ```

2. **Update Configuration:**

   - Open `appsettings.json` and `appsettings.Development.json`.
   - Update the following configuration values:
     - **Connection String:** Set your PostgreSQL connection string.
       ```json
       "ConnectionStrings": {
         "DbConnectionString": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
       }
       ```
     - **JWT Settings:** Update your JWT secret key, issuer, and audience.
       ```json
       "Jwt": {
         "Key": "YourVerySecureSecretKey",
         "Issuer": "yourdomain.com",
         "Audience": "yourdomain.com"
       }
       ```
       You can generate a JWT key from this [Website](https://jwtsecret.com/generate)
       
     - **Security Settings:**  
       **Important:** Change the `EncryptionKey` to your own secure key.
       ```json
       "Security": {
         "EncryptionKey": "YourUniqueEncryptionKeyHere",
         "SystemAdminPassword": "YourSystemAdminPasswordHere"
       }
       ```

5. **Run the Application:**

   Use the Makefile to run migrations, update the database, or run the application. For example, to update the database:
   ```bash
   make db-update
   ```
   And to run the application:
   ```bash
   make run
   ```

## Architecture Overview

- **Domain:** Contains entities and business logic.
- **Application:** Implements CQRS, UnitOfWork, and application services.  
  The `IJwtService` interface is defined here for token generation/validation.
- **Infrastructure:** Implements repositories, a generic repository, and data access via EF Core (Postgres).  
  Uses DI for each component.
- **Presentation:** Web API project containing middleware (JWT, exception handling) and controllers.

## Exception Handling & API Responses

- **Exception Handling Middleware:**  
  Automatically catches exceptions thrown during request processing and formats responses using the `ApiResponse` class.
  
- **ApiResponse Class:**  
  Use this class in your controllers to return standardized responses.

## Pagination

- **Query Extension for Pagination:**  
  A query extension is available to simplify pagination. Use it in your query handlers to easily implement paging functionality.

## Contributions & Feedback

Contributions, suggestions, and feedback are welcome!  
If you have any ideas or improvements that could help enhance this template, don't hesitate to contribute or open an issue.

## Conclusion

Stamply is built to provide a robust, enterprise-grade backend for digital loyalty programs. By leveraging DDD and Clean Architecture, it ensures that the system remains maintainable as your loyalty program grows.

Happy coding!

By Ahmad Awwad :)

