#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/helping-hand.Server.csproj", "Server/"]
COPY ["Client/helping-hand.Client.csproj", "Client/"]
COPY ["Shared/helping-hand.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/helping-hand.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "helping-hand.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "helping-hand.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "helping-hand.Server.dll"]