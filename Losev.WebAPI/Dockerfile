FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Losev.WebAPI/*.csproj Losev.WebAPI/
COPY Losev.Application/*.csproj Losev.Application/
COPY Losev.Domain/*.csproj Losev.Domain/
COPY Losev.Infrastructure/*.csproj Losev.Infrastructure/
RUN dotnet restore Losev.WebAPI/Losev.WebAPI.csproj
COPY . .
WORKDIR /src/Losev.WebAPI
RUN dotnet publish Losev.WebAPI.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Losev.WebAPI.dll"]
