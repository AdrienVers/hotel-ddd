$config = Get-Content -Path "appsettings.Docker.json" | ConvertFrom-Json
$env:ConnectionStrings__database = $config.ConnectionStrings.database
dotnet ef database update
