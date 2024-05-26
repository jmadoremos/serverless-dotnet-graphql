#!/bin/sh

##########################################
# Update dependencies
##########################################

# Update .NET solution dependencies
dotnet restore src

# Install or update .NET local tools from manifest
dotnet tool restore

##########################################
# Database migration
##########################################

# Build .NET solution
dotnet build src

# Create initial migration manifest for GraphQL project
dotnet ef migrations add Initial --project src/GraphQL

# Update database based on migration manifest for GraphQL project
dotnet ef database update --project src/GraphQL >> /dev/null
if [[ $? -eq 1 ]]; then
    dotnet ef database drop --project src/GraphQL --force

    dotnet ef database update --project src/GraphQL
fi
