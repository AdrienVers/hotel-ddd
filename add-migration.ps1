param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

Write-Host "Creating migration: $MigrationName" -ForegroundColor Green
dotnet ef migrations add $MigrationName --output-dir src/Infrastructure/Migrations
