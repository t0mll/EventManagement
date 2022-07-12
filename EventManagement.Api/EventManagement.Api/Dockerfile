FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EventManagement.Api/EventManagement.Api.csproj", "EventManagement.Api/"]
RUN dotnet restore "EventManagement.Api/EventManagement.Api.csproj"
COPY . .
WORKDIR "/src/EventManagement.Api"
RUN dotnet build "EventManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventManagement.Api.dll"]
