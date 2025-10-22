#!/bin/bash
set -e

echo "Applying EF Core migrations..."
dotnet OmniCarPark.dll --migrate

echo "Starting application..."
exec dotnet OmniCarPark.dll