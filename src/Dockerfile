##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./Directory.Packages.props", "./"]
COPY ["src/Platform.Api/Platform.Api.csproj", "src/Platform.Api/"]
COPY ["src/Platform.Application/Platform.Application.csproj", "src/Platform.Application/"]
COPY ["src/Platform.Domain/Platform.Domain.csproj", "src/Platform.Domain/"]
COPY ["src/Platform.Infrastructure/Platform.Infrastructure.csproj", "src/Platform.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Platform.Api"

FROM build AS publish
RUN dotnet publish "Platform.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID 
ENTRYPOINT ["dotnet", "Platform.Api.dll"]