#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Nexer.Finance.Application/Nexer.Finance.Application.csproj", "src/Nexer.Finance.Application/"]
RUN dotnet restore "./src/Nexer.Finance.Application/Nexer.Finance.Application.csproj"
COPY . .
WORKDIR "/src/src/Nexer.Finance.Application"
RUN dotnet build "./Nexer.Finance.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Nexer.Finance.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nexer.Finance.Application.dll"]