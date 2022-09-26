FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GB-API/GB-API.csproj", "GB-API/"]
RUN dotnet restore "GB-API/GB-API.csproj"
COPY . .
WORKDIR "/src/GB-API"
RUN dotnet build "GB-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GB-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GB-API.dll"]
