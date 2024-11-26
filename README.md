# Platform APIs

The rest APIs are the talent cloud platform.

## Prerequisites

1. .NET 8
2. PostgreSQL
3. Restore dotnet tools `dotnet tool restore`
4. Azure

## Configuration

Following the are configurations to be made

1. **ConnectionStrings\_\_DefaultConnection**: The database connection string to PostgreSQL database
2. **AzureBlobStorage\_\_ConnectionString**: The connection string to Azure Blob Storage 

## Environment variables

Copy `.env.example` to `.env` and fill in the following values, which are used by the application and that are not common among all developers.

1. **ConnectionStrings\_\_DefaultConnection**: The database connection string to PostgreSQL database
1. **AzureBlobStorage\_\_ConnectionString**: The connection string to Azure Blob Storage
1. **FRONTEND_URL**: The frontend URL
1. **COMMUNICATION_SERVICES_CONNECTION_STRING**: The azure communication service connection string
1. **JWT_SECRET**: The JWT secret token, generate with `openssl rand -base64 32`
1. **APPLICATIONINSIGHTS_CONNECTION_STRING**: The connection string to application insights

## Running locally

```bash
cd src
dotnet watch run
```

## Code Quality maintenance

Before pushing the code make sure all are passing

```bash
dotnet csharpier .

## checks spelling
npx cspell .
```

## Docker build

```bash
docker build -t platform-api:latest .
```

## Generating migrations and updating to db

1. If changes on the entity then generate the migration files using

   ```bash
   dotnet ef migrations add MessageTitle
   ```

1. Verify the script and push to the changes manually if needed

   ```bash
   dotnet ef database update
   ```

## Known Issues
if during debug, port 5555 is used, the application will not start.
```bash
# kill the port
sudo lsof -t -i tcp:5555 | xargs kill -9
```