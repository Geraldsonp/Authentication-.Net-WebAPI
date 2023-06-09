# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the project files
COPY /AuthenticationApi/ ./AuthenticationApi/
COPY /Application/ ./Application/

# Restore the dependencies
WORKDIR /app/AuthenticationApi

RUN dotnet restore

# Build the application
RUN dotnet build -c Release --no-restore

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 3: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published files
COPY --from=publish /app/publish .

# Expose the port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "AuthenticationApi.dll"]
