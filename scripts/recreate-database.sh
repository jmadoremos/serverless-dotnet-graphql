#!/bin/sh

dotnet build src
[ $? -eq 1 ] && exit 1

dotnet ef database drop --project src/GraphQL --force

dotnet ef migrations remove --project src/GraphQL

dotnet ef migrations add Initial --project src/GraphQL

dotnet ef database update --project src/GraphQL