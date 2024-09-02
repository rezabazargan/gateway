FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore ./src/Gateway.App/Gateway.App.csproj
# Build and publish a release
RUN dotnet publish ./src/Gateway.App/Gateway.App.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

# Set the ASPNETCORE_URLS environment variable
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "Gateway.App.dll"]
