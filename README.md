# Platform API

The REST API for the TalentCloud platform, built with ASP.NET Core 8 and PostgreSQL.

## Prerequisites

1. .NET 8
2. PostgreSQL
3. Restore dotnet tools: `dotnet tool restore`
4. Azure (Blob Storage + Communication Services)

## Configuration

Copy `.env.example` to `.env` and fill in the following values:

| Variable | Description |
|----------|-------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string |
| `AzureBlobStorage__ConnectionString` | Azure Blob Storage connection string |
| `FRONTEND_URL` | Frontend application URL (used for CORS allowlist) |
| `COMMUNICATION_SERVICES_CONNECTION_STRING` | Azure Communication Services connection string |
| `JWT_SECRET` | JWT signing secret — generate with `openssl rand -base64 32` |
| `APPLICATIONINSIGHTS_CONNECTION_STRING` | Azure Application Insights connection string |

## Running Locally

```bash
cd src
dotnet watch run
```

## Code Quality

Before pushing, ensure all checks pass:

```bash
# Format C# code
dotnet csharpier .

# Check spelling
npx cspell .
```

## Docker Build

```bash
docker build -t platform-api:latest .
```

## Database Migrations

```bash
# Generate a new migration after entity changes
dotnet ef migrations add <MigrationName>

# Apply migrations to the database
dotnet ef database update
```

## Known Issues

If port 5555 is already in use during debug:

```bash
sudo lsof -t -i tcp:5555 | xargs kill -9
```

---

## Changelog

### Security Hardening — PR [#124](https://github.com/talentcloudtech/platform-api/pull/124)

> **Date:** May 2026

#### 🔴 Critical Fixes

- **CORS** — Replaced `AllowAnyOrigin()` wildcard with an explicit `FrontendUrl` allowlist and `AllowCredentials()`. Previously, any origin could make credentialed cross-origin requests to the API.

- **HTTPS Enforcement** — Set `RequireHttpsMetadata = true` in JWT bearer configuration. Was previously `false`, allowing JWT tokens to be transmitted over plain HTTP and intercepted via MITM attacks.

- **Rate Limiting** — Added ASP.NET Core `RateLimiter` middleware with two policies:
  - `auth` policy: **10 requests/minute** on login, register, forgot-password, reset-password, refresh-token, resend-confirmation, confirm-email
  - `global` policy: **200 requests/minute** API-wide
  - Returns HTTP 429 on breach — protects against brute-force, credential stuffing, and token flooding

- **Swagger Gated to Non-Production** — `UseSwaggerSetup()` is now only called when `!IsProduction()`, preventing full API schema exposure in production environments.

#### 🟠 High Severity Fixes

- **Health Check Endpoint** — Added `/health` with a PostgreSQL liveness probe via `AspNetCore.HealthChecks.NpgSql`. Enables Kubernetes readiness/liveness probes and load balancer health detection.

- **CI Security Scanning** — Added `.github/workflows/security.yml`:
  - CodeQL SAST (C#)
  - Dependency vulnerability review on every PR (fails on HIGH+)
  - TruffleHog secret scanning
  - Trivy container image scanning (fails on CRITICAL/HIGH)

#### 🟡 Medium Fixes

- **Password Policy** — Minimum password length increased from 6 → **8 characters** (NIST SP 800-63B recommendation).

- **Token Expiry** — DataProtection token lifespan reduced from 2 hours → **1 hour** (resolves the `// TODO: reduce before production` comment).

- **change-password Bug Fix** — The `/change-password` endpoint was missing the `UserId` injection from `ICurrentSession`, unlike all other protected endpoints. Now correctly scopes password changes to the authenticated user.
