#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TemplateApiNet6/TemplateApiNet6.csproj", "TemplateApiNet6/"]
RUN dotnet restore "TemplateApiNet6/TemplateApiNet6.csproj"
COPY . .
WORKDIR "/src/TemplateApiNet6"
RUN dotnet build "TemplateApiNet6.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "TemplateApiNet6.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemplateApiNet6.dll"]