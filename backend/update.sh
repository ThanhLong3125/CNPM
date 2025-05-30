#!/bin/bash

# Define the project file name
PROJECT_FILE="backend.csproj"

# --- Step 1: Update Target Framework to net9.0 ---
echo "Updating TargetFramework to net9.0 in $PROJECT_FILE..."
# Use sed to replace net8.0 with net9.0
# For macOS/BSD sed (requires -i with an empty string for inplace editing):
sed -i '' 's/<TargetFramework>net8.0<\/TargetFramework>/<TargetFramework>net9.0<\/TargetFramework>/g' "$PROJECT_FILE"
# For GNU sed (Linux, requires no empty string with -i):
# sed -i 's/<TargetFramework>net8.0<\/TargetFramework>/<TargetFramework>net9.0<\/TargetFramework>/g' "$PROJECT_FILE"
echo "TargetFramework updated."
echo "---"

# --- Step 2: Update NuGet Packages to .NET 9 compatible versions ---
echo "Updating NuGet packages to latest .NET 9 compatible versions..."

# List of packages to update
packages=(
      "BCrypt.Net-Next"
      "Microsoft.AspNetCore.Authentication.JwtBearer"
      "Microsoft.EntityFrameworkCore"
      "Microsoft.EntityFrameworkCore.Design"
      "Microsoft.EntityFrameworkCore.Tools"
      "Npgsql.EntityFrameworkCore.PostgreSQL"
      "Swashbuckle.AspNetCore"
      "Swashbuckle.AspNetCore.Annotations"
      "System.IdentityModel.Tokens.Jwt"
)

for package in "${packages[@]}"; do
      echo "Updating $package..."
      # Using --version latest will pick up the latest available, including previews if you have .NET 9 SDK installed
      dotnet add "$PROJECT_FILE" package "$package" --version 9.0.0-rc.1 # Explicitly target RC1
      # Alternatively, you could try:
      # dotnet add "$PROJECT_FILE" package "$package" --version latest --prerelease
      # But explicitly targeting RC1 is often safer for consistency across packages.
      if [ $? -ne 0 ]; then
            echo "Failed to update $package. Please check for specific package versions or errors."
      fi
done

echo "---"
echo "All specified NuGet packages have been updated (or attempted to be updated)."

# --- Step 3: Run dotnet restore and build to verify ---
echo "Running dotnet restore..."
dotnet restore

echo "---"
echo "Running dotnet build..."
dotnet build

if [ $? -eq 0 ]; then
      echo "---"
      echo "Project built successfully with .NET 9 (preview)!"
else
      echo "---"
      echo "Build failed. Please review the output above for errors."
      echo "You might need to adjust specific package versions or resolve breaking changes."
fi
