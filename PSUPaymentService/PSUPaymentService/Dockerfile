# Based on https://dev.to/schwamster/docker-tutorial-with-for-aspnet-core
# Build image
FROM microsoft/aspnetcore-build:2.0 as build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish --output /out/ --configuration Release

# Runtime image
FROM microsoft/aspnetcore:2.0

WORKDIR /app
# Copies from our build image's out folder to current workdir /app
COPY --from=build /out .
ENTRYPOINT [ "dotnet", "PSUPaymentService.dll" ]