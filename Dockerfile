ARG APP_UID=1001

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY ["src/Platform.csproj", "src/"]
RUN dotnet restore "src/Platform.csproj"
COPY . .
WORKDIR "/src/src"

FROM build AS publish
RUN dotnet publish "Platform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG APP_UID
USER ${APP_UID}
ENTRYPOINT ["dotnet", "Platform.dll"]
