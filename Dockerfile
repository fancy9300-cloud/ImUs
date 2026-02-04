FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["ImUs.sln", "./"]
COPY ["src/ImUs.Web/ImUs.Web.csproj", "src/ImUs.Web/"]
COPY ["src/ImUs.Application/ImUs.Application.csproj", "src/ImUs.Application/"]
COPY ["src/ImUs.Infrastructure/ImUs.Infrastructure.csproj", "src/ImUs.Infrastructure/"]
COPY ["src/ImUs.Domain/ImUs.Domain.csproj", "src/ImUs.Domain/"]
COPY ["tests/ImUs.Tests/ImUs.Tests.csproj", "tests/ImUs.Tests/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish
WORKDIR "/src/src/ImUs.Web"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ImUs.Web.dll"]
