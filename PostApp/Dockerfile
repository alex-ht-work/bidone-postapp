#Note: This dockerfile should be built in the context of the solution folder, not the project folder where it sits.
#      Use the solution folder as a docker build contxt and specify the path to this file by -f option
#
#      Or use docker compose with the compose file provided.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY PostApp/PostApp.csproj PostApp/
RUN dotnet restore "PostApp/PostApp.csproj"

COPY PostApp/ PostApp/

WORKDIR "/src/PostApp"
RUN dotnet build "PostApp.csproj" -c Release -o /app/build -p:SkipNode=true

FROM build AS publish
RUN dotnet publish "PostApp.csproj" -c Release -o /app/publish -p:SkipNode=true

FROM node:18.14 AS build-frontend
WORKDIR /build

COPY PostApp/ClientApp/package*.json ./
RUN npm ci

COPY PostApp/ClientApp/ .

RUN npm run build --prod

FROM base AS final
RUN apt-get update && apt-get install -y curl # this is for health checks
HEALTHCHECK --interval=10s --timeout=5s --start-period=10s --retries=3 CMD curl --fail -s http://localhost:80/

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-frontend /build/dist/ ./wwwroot

ENTRYPOINT ["dotnet", "PostApp.dll"]