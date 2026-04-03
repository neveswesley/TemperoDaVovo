# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore ./src/BackEnd/TemperoDaVovo.API/TemperoDaVovo.API.csproj
RUN dotnet publish ./src/BackEnd/TemperoDaVovo.API/TemperoDaVovo.API.csproj -c Release -o /app/out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TemperoDaVovo.API.dll"]