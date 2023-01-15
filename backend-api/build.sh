#!/bin/bash

# Build the .NET project
dotnet build -c Release

# Publish the .NET project
dotnet publish -c Release -r linux-x64 -o out

# Build the Docker image
docker build -t foodwaste-backend-api:latest .

# Run the docker image
#docker run -d -p 3001:80 foodwaste-backend-api:latest