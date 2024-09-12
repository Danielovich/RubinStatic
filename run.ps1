# Get the full path to the executable and working directory
$currentDir = Get-Location
$exePath = Resolve-Path "src\Rubin.Static.Console\bin\Release\net8.0\Rubin.Static.Console.exe"
$workingDir = Resolve-Path "src\Rubin.Static.Console\bin\Release\net8.0"

# Check if the executable exists
if (Test-Path $exePath) {
    # Change to the directory containing the appsettings.json
    Set-Location $workingDir

    # Execute the exe with the 'generate' parameter
    & $exePath generate
} else {
    Write-Host "Executable not found at $exePath"
}

Set-Location $currentDir