#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["DragonBugs2020.csproj", ""]
RUN dotnet restore "./DragonBugs2020.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DragonBugs2020.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonBugs2020.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DragonBugs2020.dll