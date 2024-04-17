FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Forum.PostApi/Forum.PostApi.csproj", "Forum.PostApi/"]
RUN dotnet restore "Forum.PostApi/Forum.PostApi.csproj"
COPY . .
WORKDIR "/src/Forum.PostApi"
RUN dotnet build "Forum.PostApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.PostApi.csproj" -c Release -o /app/publish


WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for-cassandra.sh ./
RUN chmod +x ./wait-for-cassandra.sh  # Make sure the script is executable
ENTRYPOINT ["./wait-for-cassandra.sh", "&&", "dotnet", "Forum.PostApi.dll"]