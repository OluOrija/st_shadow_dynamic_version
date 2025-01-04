# Use the .NET 8 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining application files and build the project
COPY . ./
RUN dotnet publish -c Release -o /out

# Use the .NET 8 runtime for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output to the runtime image
COPY --from=build /out .

# Set the environment variable for ASP.NET Core to listen on all network interfaces
ENV ASPNETCORE_URLS=http://+:5008;https://+:7103

# Copy certificates for HTTPS (ensure you have valid certificates)
COPY ./certs/server.pfx /https/server.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/server.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=stshadowdev

# Expose ports for HTTP and HTTPS
EXPOSE 5008
EXPOSE 7103

# Define the entry point for the container
ENTRYPOINT ["dotnet", "stshadowbackend.dll"]
