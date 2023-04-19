#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./RANO.csproj" --disable-parallel
RUN dotnet publish "./RANO.csproj" -c release -o /app --no-restore



#Serve
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build / app ./

EXPOSE 5000
EXPOSE 8080


ENTRYPOINT ["dotnet", "RANO.dll"]

