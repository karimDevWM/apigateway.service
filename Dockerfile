# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source
EXPOSE 5000

COPY ./*.csproj ./apigateway.service/
RUN dotnet restore ./apigateway.service/*.csproj

COPY . ./apigateway.service/
WORKDIR /source/apigateway.service
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "apigateway.service.dll" ]