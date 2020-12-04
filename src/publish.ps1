$current = Get-Location
$target = "$current\Publish\"

dotnet publish --configuration Release -o $target 