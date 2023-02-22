---
uid: nuget-restore
title: "Restoring Packages at Build Time"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Restoring Packages at Build Time

NuGet Package Manager has the ability to restore packages from their repository during the build. This allows teams to avoid storing NuGet packages in their source repository.

You can restore the PostSharp package at build time as long as the package is restored before MSBuild is invoked to build the project.

The reason is that the *PostSharp.targets* file is required during the build, otherwise PostSharp is not inserted in the build process, and simply does not work. Because of the design of MSBuild, *PostSharp.targets* must be present when the build starts, so it cannot be restored from the package repository during the same build. The build that triggers the package restore will either fail or run without PostSharp, and subsequent builds will succeed. A rebuild is then required. 

This behavior is acceptable on developer workstations. However, on build servers, you must ensure that the packages are restored *before* the project is built. 


## NuGet 2.7 and Later

To restore the PostSharp package at build time, add a preliminary step before building the Visual Studio solutions or projects. This step should execute the following command:

```powershell
NuGet.exe restore MySolution.sln
```

In this command, the *MySolution.sln* is the solution for which packages have to be restored. 

To restore packages for a solution where some projects use the packages.config package management format and others use the PackageReference package management format, use this way as well. It will restore packages in all the projects, regardless of the package management format used.

See [NuGet Command-Line Reference](http://docs.nuget.org/docs/reference/command-line-reference) for details. 


## Visual Studio 2017 and Later

If all your projects use the PackageReference package management format, you can use the MSBuild Restore target to restore NuGet packages. To restore the PostSharp package at build time, add a preliminary step before building the Visual Studio solutions or projects. This step should execute the following command:

```powershell
MSBuild /T:Restore MySolution.sln
```

In this command, the *MySolution.sln* is the solution for which packages have to be restored. Eventually, you can call the MSBuild target from your MSBuild script. 

> [!WARNING]
> If some projects of your solution use the packages.config project management format, those will not get the NuGet packages restored this way. Use the NuGet.exe command described in the first section instead.

See [NuGet pack and restore as MSBuild targets](https://docs.microsoft.com/en-us/nuget/schema/msbuild-targets#restore-target) for details. 


## .NET Core Command Line Interface

If all your projects are .NET Core projects, you can use the .NET Core Command Line Interface to restore NuGet packages. To restore the PostSharp package at build time, add a preliminary step before building the Visual Studio solutions or projects. This step should execute the following command:

```powershell
dotnet restore MySolution.sln
```

In this command, the *MySolution.sln* is the solution for which packages have to be restored. 

> [!WARNING]
> If some projects of your solution are not .NET Core projects, those will not get the NuGet packages restored this way correctly. Use the NuGet.exe command described in the first section instead.

See [dotnet-restore](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-restore) for details. 


## NuGet 2.0 to 2.6

To restore the PostSharp package at build time, add a preliminary step before building the Visual Studio solutions or projects. This step should execute the following command for every *packages.config* file in your solution (typically, for every project): 

```powershell
NuGet.exe install packages.config -OutputDirectory SolutionDirectory\packages
```

In this command, where *SolutionDirectory\packages* is the directory where the NuGet packages should be installed. 

Please look at the [NuGet Command-Line Reference](http://docs.nuget.org/docs/reference/command-line-reference) for details. 

> [!TIP]
> You can use PowerShell or MSBuild to execute the <code>nuget install</code>
 command to all *packages.config* files in your source repository. 

## See Also

**Other Resources**

[Package Restore on NuGet Reference Documentation](http://docs.nuget.org/docs/reference/package-restore)
<br>