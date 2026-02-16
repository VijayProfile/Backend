FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet publish SqlServer/SqlServer.csproj -c Release -o out
WORKDIR /app/out
CMD ["dotnet", "SqlServer.dll"]
