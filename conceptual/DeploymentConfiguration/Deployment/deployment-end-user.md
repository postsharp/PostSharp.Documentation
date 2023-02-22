---
uid: deployment-end-user
title: "Deploying PostSharp to End-User Devices"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Deploying PostSharp to End-User Devices

Although PostSharp is principally a compiler technology, it contains run-time libraries that need to be deployed along with your application to end-user devices.


## Redistribution of PostSharp run-time libraries without NuGet

If you do not deliver your final product using NuGet, you can bundle all the referenced PostSharp run-time libraries in your product. These libraries are the ones included in the *lib* subdirectory of the NuGet packages and the ZIP distribution. 

These run-time libraries can be distributed to end-users free of charge. However, the build-time parts of PostSharp cannot be redistributed under the terms of the standard license agreement.

Besides including these run-time libraries, no other action or configuration is required.


## Redistribution of PostSharp run-time libraries using NuGet

If you deliver your final product using NuGet, you can add PostSharp redistributable NuGet packages as a dependency to your NuGet package instead of including all the run-time libraries inside your NuGet package.

PostSharp NuGet packages are either intended for build-time or for run-time. We do not mix both in any of our NuGet packages. See <xref:postsharp-components> for a description of how to distinguish them. 

Thanks to this fact, you only need to set the run-time PostSharp NuGet packages as a dependency of your NuGet package.


### Sample NuGet package

In the following example, there is a NuGet package specification of a package which has the PostSharp.Redist package as its dependency. This way, there's no need to include the PostSharp run-time libraries inside the package.

```xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd">
  <metadata>
    <id>MyLibrary</id>
    <version>1.0.0</version>
    <authors>John Smith</authors>
    <owners>John Smith</owners>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>This is my library created with a help of PostSharp.</description>
    <dependencies>
      <!-- TODO: Replace X.X.X with a version of PostSharp you want to be dependent. -->
      <dependency id="PostSharp.Redist" version="X.X.X" />
    </dependencies>
  </metadata>
  <files>
    <file src="bin\Release\MyLibrary.dll" target="lib\net46"/>
    <!-- NOTE: No need to include PostSharp run-time libraries inside the package. -->
  </files>
</package>
```

See [.nuspec reference](https://docs.microsoft.com/en-us/nuget/schema/nuspec#dependencies) for details. 


### Using the Pack MSBuid target or .NET Core CLI

In the following example, you see a .NET Core project which uses the PackageReference package management format. Creating a NuGet package using the *Pack* MSBuid target or .NET Core CLI command `dotnet pack` from this project will create a NuGet package which will depend on the run-time PostSharp packages only. 

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp1.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
  
    <!-- TODO: Replace X.X.X with a version of PostSharp you want to be dependent. -->
    <PackageReference Include="PostSharp" Version="X.X.X">
      <!-- NOTE: Thanks to the following element, the PostSharp build-time NuGet package
                 will not be a dependency of the NuGet package created from this project. -->
      <PrivateAssets>All</PrivateAssets>
    </PackageReference>
    
    <!-- TODO: Replace X.X.X with a version of PostSharp you want to be dependent. -->
    <PackageReference Include="PostSharp.Redist" Version="X.X.X" />
    <!-- NOTE: The PostSharp.Redist run-time NuGet package
               will be a dependency of the NuGet package created from this project. -->
    
  </ItemGroup>
  
</Project>
```

See [Package references (PackageReference) in project files](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files) for details. 

