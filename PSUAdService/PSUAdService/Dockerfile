# Based on https://dev.to/schwamster/docker-tutorial-with-for-aspnet-core
# Build image 1.8 GB with stuff build with
FROM microsoft/aspnetcore-build:2.0 as build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish --output /out/ --configuration Release

# Runtime image 280 mb
FROM microsoft/aspnetcore:2.0
# Install gdipluss https://github.com/zkweb-framework/ZKWeb.System.Drawing
# Gdipluss is a dependency for the ZKWeb.System.Drawing.dll
# The ZKWeb.System.Drawing framework might be deprecated soon, when the dotnet team finishes the system.drawing functionality
RUN apt-get update
RUN apt-get install libgdiplus -y
RUN ln -s libgdiplus.so gdiplus.dll 
WORKDIR /app
# Copies from our build image's out folder to current workdir /app
COPY --from=build /out .
ENTRYPOINT [ "dotnet", "PSUAdService.dll" ]