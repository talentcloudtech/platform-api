# Platform APIs

The rest APIs are the talent cloud platform.

## Prerequisites

1. .NET 8
2. PostgreSQL
3. Restore dotnet tools `dotnet tool restore`

## Configuration

Following the are configurations to be made

1. **ConnectionStrings\_\_DefaultConnection**: The database connection string to PostgreSQL database

## Running locally

```bash
cd src
dotnet watch run
```


## Code Quality maintenance

Before pushing the code make sure all are passing

```bash
dotnet csharpier .
dotnet format style
dotnet format analyzers
```
