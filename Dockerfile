FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Slimer/Slimer.csproj", "Slimer/"]
COPY ["nuget.config", ""]

RUN dotnet restore "Slimer/Slimer.csproj"

WORKDIR $HOME/src
COPY . .
RUN dotnet build "Slimer/Slimer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Slimer/Slimer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Slimer.dll"]