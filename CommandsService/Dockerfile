FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
# restore dependencies
RUN dotnet restore

# copy all files to the working directory
COPY . ./

# build the project
RUN dotnet publish -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# configure the container to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# copy the build output to the final image
COPY --from=build-env /app/out .
# set the entry point for the container
ENTRYPOINT ["dotnet", "CommandsService.dll"]