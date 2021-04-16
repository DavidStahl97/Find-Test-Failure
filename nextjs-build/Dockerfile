#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/backend/TestFramework.WebAPI/TestFramework.WebAPI.csproj", "src/backend/TestFramework.WebAPI/"]
COPY ["src/backend/TestFramework.Infrastructure/TestFramework.Infrastructure.csproj", "src/backend/TestFramework.Infrastructure/"]
COPY ["src/TestFramework.Contract/TestFramework.Contract.csproj", "src/TestFramework.Contract/"]
COPY ["src/backend/TestFramework.Application/TestFramework.Application.csproj", "src/backend/TestFramework.Application/"]
COPY ["src/backend/TestFramework.Domain/TestFramework.Domain.csproj", "src/backend/TestFramework.Domain/"]
COPY ["src/frontend/TestFramework.Client/TestFramework.Client.csproj", "src/frontend/TestFramework.Client/"]
COPY ["src/frontend/TestFramework.Client.WebAPI/TestFramework.Client.WebAPI.csproj", "src/frontend/TestFramework.Client.WebAPI/"]
COPY ["src/TestFramework.Utils/TestFramework.Utils.csproj", "src/TestFramework.Utils/"]
RUN dotnet restore "src/backend/TestFramework.WebAPI/TestFramework.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/backend/TestFramework.WebAPI"
RUN dotnet build "TestFramework.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestFramework.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestFramework.WebAPI.dll"]