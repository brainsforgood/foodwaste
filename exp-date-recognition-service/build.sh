#!/bin/bash

# Build the .NET project
dotnet build -c Release

# Publish the .NET project
dotnet publish -c Release -o out

# Build the Docker image
docker build -t exp-date-recognition-service:latest .