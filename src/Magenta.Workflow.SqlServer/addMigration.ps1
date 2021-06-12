param (
    [string]$p1
 )
#--startup-project ../Samples/WebApp/WebApp.csproj
dotnet ef migrations add $p1 --startup-project ../Samples/WebApp/WebApp.csproj --verbose -o Migrations -c WorkflowDbContext