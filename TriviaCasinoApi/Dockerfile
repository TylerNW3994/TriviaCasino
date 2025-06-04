FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /TriviaCasinoApi

# Copy the app package and package-lock.json file
COPY package*.json ./

# Copy local directories to the current local directory of our docker image
COPY . .

# RUN dotnet restore
# RUN dotnet build

ENV PORT=5000
EXPOSE 5000

# Start the app using serve command
CMD [ "dotnet", "run", "TriviaCasinoApi.csproj" ]
