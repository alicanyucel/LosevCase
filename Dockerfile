# Use the official .NET 9 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Losev/Losev.WebAPI/*.csproj Losev/Losev.WebAPI/
COPY Losev/Losev.Application/*.csproj Losev/Losev.Application/
COPY Losev/Losev.Domain/*.csproj Losev/Losev.Domain/
COPY Losev/Losev.Infrastructure/*.csproj Losev/Losev.Infrastructure/
RUN dotnet restore Losev/Losev.WebAPI/Losev.WebAPI.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/Losev/Losev.WebAPI
RUN dotnet publish Losev.WebAPI.csproj -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Losev.WebAPI.dll"]
