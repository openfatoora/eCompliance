# Stage 1: Build React + Vite frontend
FROM node:20 AS frontend-build

WORKDIR /app

# Copy frontend dependencies
COPY efatoora.client/package.json efatoora.client/yarn.lock ./
RUN yarn install

# Copy and build the frontend application
COPY efatoora.client .
RUN yarn build

# Stage 2: Build .NET backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build

WORKDIR /app

# Copy and restore backend dependencies
COPY efatoora.Server/efatoora.Server.csproj efatoora.Server/
COPY efatoora.service/efatoora.service.csproj efatoora.service/
RUN dotnet restore efatoora.Server/efatoora.Server.csproj

# Copy the rest of the backend code
COPY efatoora.Server efatoora.Server
COPY efatoora.service efatoora.service

# Publish the backend application
RUN dotnet publish efatoora.Server/efatoora.Server.csproj -c Release -o /app/publish

# Stage 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
EXPOSE 8080
EXPOSE 5173

# Copy built frontend and published backend into final image
COPY --from=frontend-build /app/dist wwwroot/
COPY --from=backend-build /app/publish .

ENTRYPOINT ["dotnet", "efatoora.Server.dll"]
