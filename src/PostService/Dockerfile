# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY PostService/*.csproj .
RUN dotnet restore "PostService.csproj"
COPY PostService/. .
RUN dotnet publish "PostService.csproj" -c release -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PostService.dll"]
