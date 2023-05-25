FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore
COPY . ./
RUN dotnet publish -c release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build  /app ./
EXPOSE 80
ENV ASPNETCORE_URLS http://+:80
ENTRYPOINT ["dotnet", "ProductManager.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductManager/ProductManager.csproj", "ProductManager/"]
RUN dotnet restore "ProductManager/ProductManager.csproj"
COPY . .
WORKDIR "/src/ProductManager"
RUN dotnet build "ProductManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManager.dll"]