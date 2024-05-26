# TODO: Update with dotnet version
FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# Set variables
ARG AMAZON_LAMBDA_TEMPLATES_VERSION=7.1.1

ARG SERVERLESSFRAMEWORK_VERSION=3.38.0

# Set current user to vscode
USER vscode

# Update APT repository
ENV DEBIAN_FRONTEND=noninteractive

#RUN sudo apt-get update

# Disable .NET Tools telemetry
# ref. https://learn.microsoft.com/en-us/dotnet/core/tools/telemetry
RUN echo '' >> $HOME/.zshrc && \
    echo '# Disable .NET Tools telemetry' >> $HOME/.zshrc && \
    echo 'DOTNET_CLI_TELEMETRY_OPTOUT=true' >> $HOME/.zshrc

ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

# Update all installed workloads to the newest available versions
RUN sudo dotnet workload update

# Install AWS Lambda templates for project creation
RUN dotnet new install Amazon.Lambda.Templates::$AMAZON_LAMBDA_TEMPLATES_VERSION

# Add .NET tools to terminal profile
RUN echo '' >> $HOME/.zshrc && \
    echo '# Add .NET Core SDK tools' >> $HOME/.zshrc && \
    echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> $HOME/.zshrc

ENV PATH="${PATH}:${HOME}/.dotnet/tools"

# Disable Serverless Framework telemetry
RUN echo '' >> $HOME/.zshrc && \
    echo '# Disable Serverless Framework telemetry' >> $HOME/.zshrc && \
    echo 'SLS_TELEMETRY_DISABLED="1 sls deploy"' >> $HOME/.zshrc

ENV SLS_TELEMETRY_DISABLED="1 sls deploy"

# Install the latest version
RUN curl -o- -L https://slss.io/install | VERSION=$SERVERLESSFRAMEWORK_VERSION bash

# Add Serverless Framework reference to terinal profile
RUN echo '' >> $HOME/.zshrc && \
    echo '# Added by serverless binary installer' >> $HOME/.zshrc && \
    echo 'export PATH="$PATH:$HOME/.serverless/bin"' >> $HOME/.zshrc

ENV PATH="${PATH}:${HOME}/.serverless/bin"

# Create a dev certificate
RUN dotnet dev-certs https --trust
