#!/bin/sh

##########################################
# Update workload
##########################################

# Disable .NET Tools telemetry
# ref. https://learn.microsoft.com/en-us/dotnet/core/tools/telemetry
cat << EOF >> ~/.zshrc
# Disable .NET Tools telemetry
DOTNET_CLI_TELEMETRY_OPTOUT=true
EOF

# Update all installed workloads to the newest available versions
sudo dotnet workload update

##########################################
# Install AWS Lambda Tools
##########################################

# Install AWS Lambda templates for project creation
dotnet new install Amazon.Lambda.Templates

# Install Amazon.Lambda.Tools
dotnet tool install --global Amazon.Lambda.Tools

# Install .NET test tools
dotnet tool install --global Amazon.Lambda.TestTool-8.0 # TODO: Update with dotnet version

# Add .NET tools to terminal profile
cat << EOF >> ~/.zshrc
# Add .NET Core SDK tools
export PATH="$PATH:$HOME/.dotnet/tools"
EOF

# Make available in the current session
export PATH="$PATH:$HOME/.dotnet/tools"

##########################################
# Install Serverless Framework
##########################################

# Disable Serverless Framework telemetry
cat << EOF >> ~/.zshrc
# Disable Serverless Framework telemetry
SLS_TELEMETRY_DISABLED=1 sls deploy
EOF

# Install the latest version
curl -o- -L https://slss.io/install | bash

# Add Serverless Framework reference to terinal profile
cat << EOF >> ~/.zshrc
# Added by serverless binary installer
export PATH="$PATH:$HOME/.serverless/bin"
EOF

# Make available in the current session
export PATH="$PATH:$HOME/.serverless/bin"

# Enable auto updates
serverless config --autoupdate

##########################################
# Dev Certificate
##########################################

# Create a dev certificate
dotnet dev-certs https --trust
