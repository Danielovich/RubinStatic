# taken from https://github.com/jbogard/MediatR/blob/master/Build.ps1

function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

exec { & dotnet clean -c Release }

exec { & dotnet build -c Release }

tree /F

exec { & dotnet test -c Release --no-build -l trx --verbosity=normal }