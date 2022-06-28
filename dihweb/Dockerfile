#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 3000
#CMD ["dotnet", "run", "--server.urls", "http://localhost:5000"]

ENV ASPNETCORE_URLS=http://*:3000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["dihweb.csproj", "dihweb/"]
RUN dotnet restore "dihweb/dihweb.csproj"
COPY . "dihweb"
WORKDIR "/src/dihweb"
RUN dotnet build "dihweb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dihweb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dihweb.dll"]