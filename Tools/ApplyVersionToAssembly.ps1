##-----------------------------------------------------------------------
## <copyright file="ApplyVersionToAssemblies.ps1">(c) Microsoft Corporation. This source is subject to the Microsoft Permissive License. See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx. All other rights reserved.</copyright>
##-----------------------------------------------------------------------
# Look for a 0.0.0.0 pattern in the build number. 
# If found use it to version the assemblies.

# Enable -Verbose option
[CmdletBinding()]
Param()

# Make sure path to source code directory is available
if (-not $Env:BUILD_SOURCESDIRECTORY)
{
    Write-Error ("BUILD_SOURCESDIRECTORY environment variable is missing.")
    exit 1
}
elseif (-not (Test-Path $Env:BUILD_SOURCESDIRECTORY))
{
    Write-Error "BUILD_SOURCESDIRECTORY does not exist: $Env:BUILD_SOURCESDIRECTORY"
    exit 1
}
Write-Verbose "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"

# Regular expression pattern to find the version in the build number 
# and then apply it to the assemblies
$VersionRegex = "\d+\.\d+\.\d+\.\d+"

$Version = $Env:BUILD_BUILDNUMBER + ".0"
Write-Verbose "Final assembly version number: $Version"

# Store the version in an env variable
Write-Host "##vso[task.setvariable variable=AssemblyVersion]$Version"

# Apply the version to the assembly property files
$files = gci $Env:BUILD_SOURCESDIRECTORY -recurse -include "*Properties*","My Project" | 
    ?{ $_.PSIsContainer } | 
    foreach { gci -Path $_.FullName -Recurse -include AssemblyInfo.* }
if($files)
{
    Write-Verbose "Will apply $NewVersion to $($files.count) files."

    foreach ($file in $files) {
        $filecontent = Get-Content($file)
        attrib $file -r
        $filecontent -replace $VersionRegex, $Version | Out-File $file
        Write-Verbose "$file.FullName - version applied"
    }
}
else
{
    Write-Warning "Found no files."
}