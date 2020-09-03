Write-Host "Building and running Solution"
Start-Process powershell ".\HelperScript_01_Build_And_Run.ps1"

Write-Host "Waiting 10 seconds for the service to startup"
Start-Sleep -Seconds 10

Write-Host "Calling service without Poly"
& .\HelperScript_02_Call_Without_Polly.ps1 > $null

Write-Host "Waiting 4 minutes..."
Start-Sleep -Seconds 241

Write-Host "Retrieving all open connections"
& .\HelperScript_04_Show_Connections.ps1 | Out-File -FilePath .\Results_01_Without_Polly.txt

Write-Host "Waiting 5 seconds..."
Start-Sleep -Seconds 5

Write-Host "Calling service with Poly"
& .\HelperScript_03_Call_With_Polly.ps1 > $null

Write-Host "Waiting 4 minutes..."
Start-Sleep -Seconds 241

Write-Host "Retrieving all open connections"
& .\HelperScript_04_Show_Connections.ps1 | Out-File -FilePath .\Results_02_With_Polly.txt
