#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["tests/TestFramework.TestWebApp/TestFramework.TestWebApp.csproj", "tests/TestFramework.TestWebApp/"]
RUN dotnet restore "tests/TestFramework.TestWebApp/TestFramework.TestWebApp.csproj"
COPY . .
WORKDIR "/src/tests/TestFramework.TestWebApp"
RUN dotnet build "TestFramework.TestWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestFramework.TestWebApp.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["tests/TestFramework.TestWebApp/nginx.conf", "/etc/nginx/nginx.conf"]