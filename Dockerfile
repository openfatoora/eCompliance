# Stage 1: Build the React app
FROM node:14 AS build-client
WORKDIR /src/efatoora.client

# Copy the client app files
COPY ["efatoora.client/package.json", "efatoora.client/package-lock.json", "./"]
COPY ["efatoora.client/", "./"]

# Install dependencies and build
RUN yarn run build

# Stage 2: Build and publish the .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["efatoora.Server/efatoora.Server.csproj", "efatoora.Server/"]
COPY ["efatoora.service/ZatcaCore/ZatcaCore.dll", "ZatcaCore/"]

# Restore dependencies
RUN dotnet restore "efatoora.Server/efatoora.Server.csproj"
COPY . .

# Set the working directory for subsequent commands
WORKDIR "/src/efatoora.Server"

# Publish the application
RUN dotnet publish "efatoora.Server.csproj" -c Release -o /app/publish

# Stage 3: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose port 80
EXPOSE 80

# Install and configure SSH (if required)
RUN apt-get update \
    && apt-get install -y --no-install-recommends openssh-server \
    && mkdir -p /run/sshd \
    && echo "root:Docker!" | chpasswd

# Copy the published .NET application
COPY --from=build /app/publish .

# Copy the built React app
COPY --from=build-client /app/dist ./efatoora.client/build

# Start SSH and the application (if required)
ENTRYPOINT ["/bin/bash", "-c", "/usr/sbin/sshd && dotnet efatoora.Server.dll"]
