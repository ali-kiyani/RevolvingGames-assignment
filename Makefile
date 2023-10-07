# Makefile for gRPC Server and Client

# Variables
SERVER_PROJECT = RevolvingGamesServer\RevolvingGamesServer\RevolvingGamesServer.csproj
CLIENT_PROJECT = RevolvingGames_Client\RevolvingGames_Client\RevolvingGames_Client.csproj

# Build the gRPC server
build-server:
	dotnet build $(SERVER_PROJECT)

# Run the gRPC server in a new terminal (Windows)
run-server:
	start cmd /k "dotnet run --project $(SERVER_PROJECT)"

# Build the gRPC client
build-client:
	dotnet build $(CLIENT_PROJECT)

# Run the gRPC client
run-client:
	dotnet run --project $(CLIENT_PROJECT)

# Run both server and client
run-all: build-server run-server build-client run-client

# Clean the build artifacts
clean:
	dotnet clean

# Default target
.PHONY: build-server run-server build-client run-client run-all clean
