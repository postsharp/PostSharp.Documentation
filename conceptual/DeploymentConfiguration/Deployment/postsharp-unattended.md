---
uid: postsharp-unattended
title: "Installing PostSharp Unattended"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "This document provides a detailed guide on how to install PostSharp, a user interface for Visual Studio, unattended using a script. It also includes a PowerShell 2.0 script for automation."
---
# Installing PostSharp Unattended

PostSharp is composed of a user interface (PostSharp Tools for Visual Studio) and build components (NuGet packages). NuGet packages are usually checked into source control or retrieved from a package repository at build time (see <xref:nuget-restore>), so its deployment does not require additional automation. The user interface is typically installed by each user. It does not require administrative privileges. 

> [!WARNING]
> The PostSharp user interface requires NuGet Package Manager 2.2, which is not installed by default with Visual Studio. Installing NuGet requires administrative privileges on the local machine.

You can install PostSharp automatically for a large number of users using a script.


### To install PostSharp on a machine:

1. Ensure that NuGet 2.2 is installed. Your script will need to look for a file named *NuGet.Core.dll* under the following directories. The search should include up to 2 levels of subdirectories. 

    * *C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\Extensions* for Visual Studio 2010. 

    * *C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\Extensions* for Visual Studio 2012. 


2. If NuGet 2.2 is not installed, install it with the command line <code>VsixInstaller.exe /q NuGet.Tools.vsix</code>
. This command requires administrative privileges on the local machine. The current version of *NuGet.Tools.vsix* can be downloaded from [Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c). Note that PostSharp is tested with versions that are current at the time of writing. If you need to be able to restore a working development environment several years from now, it is a good idea to archive a version of NuGet that is known to work with your version of PostSharp. 


3. Execute command line <code>VsixInstaller.exe /q PostSharp-VERSION.vsix</code>
. The file can be downloaded from [Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/a058d5d3-e654-43f8-a308-c3bdfdd0be4a). Exit codes other than `0` or `1001` should be considered as errors. 


4. Install the license key or the license server URL in the registry key `HKEY_CURRENT_USER\Software\SharpCrafters\PostSharp 3`, registry value `LicenseKey` (type `REG_SZ`). 


This procedure can be automated by the following PowerShell 2.0 script:

```powershell
# TODO: Set the right value for the following variables
$postsharpFile = "PostSharp-3.0.14-beta.vsix"   # Replace with the proper version number and add the full path.
$nugetFile = "NuGet.Tools.vsix"                 # Add the full path.
$license = "XXXX-XXXXXXXXXXXXXXXXXXXXXXXXX"     # Replace by your license key or license server URL.


$installNuget = $false

# Check NuGet in Visual Studio 2010.
if ( Test-Path "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" ) 
{

    $vsixInstaller = "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\VsixInstaller.exe"

    $nugetVs10Path = dir "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\Extensions" -Include "NuGet.Core.dll" -Recurse | select -First 1

    if ( $nugetVs10Path -eq $null )
    {
        $installNuget = $true                          
    }
    else
    {
        $nugetVs10Version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($nugetVs10Path)
        Write-Host "Detected NuGet" $nugetVs10Version.FileVersion "for Visual Studio 2010."
        if ( $nugetVs10Version.FileMajorPart -lt 2 -or $nugetVs10Version.FileMinorPart -lt 1 )
        {
            $installNuget = $true;
        }
    }
}

# Check NuGet in Visual Studio 2012.
if ( Test-Path "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe" ) 
{

    $vsixInstaller = "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\VsixInstaller.exe"
    $nugetVs11Path = dir "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\Extensions" -Include "NuGet.Core.dll" -Recurse | select -First 1

    if ( $nugetVs11Path -eq $null )
    {
        $installNuget = $true                          
    }
    else
    {
        $nugetVs11Version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($nugetVs10Path)
        Write-Host "Detected NuGet"  $nugetVs11Version.FileVersion "for Visual Studio 2012."
        if ( $nugetVs11Version.FileMajorPart -lt 2 -or $nugetVs11Version.FileMinorPart -lt 1 )
        {
            $installNuget = $true;
        }
    }
}


if ( -not ( Test-Path $vsixInstaller) )
{
    Write-Host "Cannot find " $vsixInstaller
    exit
}

# Install NuGet
if ( $installNuget )
{
    Write-Host "Installing NuGet"
    $process = Start-Process -FilePath  $vsixInstaller -ArgumentList @("/q", $nugetFile) -Wait -Verb runas  -PassThru
    if ( $process.ExitCode -ne 0 -and $process.ExitCode -ne 1001)
    {
        Write-Host "Error: VsixInstaller exited with code" $process.ExitCode -ForegroundColor Red
    }
}


# Install PostSharp
Write-Host "Installing PostSharp"
$process = Start-Process -FilePath $vsixInstaller -ArgumentList @("/q", $postsharpFile) -Wait -PassThru
if ( $process.ExitCode -ne 0 -and $process.ExitCode -ne 1001)
{
    Write-Host "Error: VsixInstaller exited with code" $process.ExitCode -ForegroundColor Red
}


# Install the license key
Write-Host "Installing the license key"
$regPath = "HKCU:\Software\SharpCrafters\PostSharp 3"


if ( -not ( Test-Path $regPath ) )
{
    New-Item -Path $regPath | Out-Null
}

Set-ItemProperty -Path $regPath -Name "LicenseKey" -Value $license


Write-Host "Done"
```


