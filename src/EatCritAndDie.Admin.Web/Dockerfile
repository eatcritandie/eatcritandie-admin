FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EatCritAndDie.Admin.Web/EatCritAndDie.Admin.Web.csproj", "EatCritAndDie.Admin.Web/"]
RUN dotnet restore "src/EatCritAndDie.Admin.Web/EatCritAndDie.Admin.Web.csproj"
COPY . .
WORKDIR "/src/EatCritAndDie.Admin.Web"
RUN dotnet build "EatCritAndDie.Admin.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EatCritAndDie.Admin.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EatCritAndDie.Admin.Web.dll"]
