#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#ok

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/WoodGroveGroceriesWebApplication/WoodGroveGroceriesWebApplication.csproj", "src/WoodGroveGroceriesWebApplication/"]
RUN dotnet restore "src/WoodGroveGroceriesWebApplication/WoodGroveGroceriesWebApplication.csproj"
COPY . .
WORKDIR "/src/src/WoodGroveGroceriesWebApplication"
RUN dotnet build "WoodGroveGroceriesWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WoodGroveGroceriesWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WoodGroveGroceriesWebApplication.dll"]