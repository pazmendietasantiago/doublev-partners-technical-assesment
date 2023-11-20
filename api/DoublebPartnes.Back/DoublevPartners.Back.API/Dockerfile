FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DoublebPartnes.Back.API/DoublebPartnes.Back.API.csproj", "DoublebPartnes.Back.API/"]
RUN dotnet restore "DoublebPartnes.Back.API/DoublebPartnes.Back.API.csproj"
COPY . .
WORKDIR "/src/DoublebPartnes.Back.API"
RUN dotnet build "DoublebPartnes.Back.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DoublebPartnes.Back.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DoublebPartnes.Back.API.dll"]
