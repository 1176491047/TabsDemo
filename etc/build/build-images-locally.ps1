param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"

$dbMigratorFolder = Join-Path $slnFolder "src/Demo.DbMigrator"

Write-Host "********* BUILDING DbMigrator *********" -ForegroundColor Green
Set-Location $dbMigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/demo-db-migrator:$version .


$blazorFolder = Join-Path $slnFolder "src/Demo.Blazor"
$hostFolder = Join-Path $slnFolder "src/Demo.HttpApi.Host"

Write-Host "********* BUILDING Blazor Application *********" -ForegroundColor Green
Set-Location $blazorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/demo-blazor:$version .





### ALL COMPLETED
Write-Host "COMPLETED" -ForegroundColor Green
Set-Location $currentFolder