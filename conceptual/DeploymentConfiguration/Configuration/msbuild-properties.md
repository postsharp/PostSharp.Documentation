---
uid: msbuild-properties
title: "Well-Known MSBuild Properties"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Well-Known MSBuild Properties

> [!NOTE]
> The integration of PostSharp with MSBuild is implemented in files *PostSharp.tasks* and *PostSharp.targets*. These files define some properties and items that are not documented here. They are considered implementation details and may change without notice. 


## General Properties

The following properties are most commonly overwritten. They can also be edited in Visual Studio using the PostSharp project property page.

| Property Name | Description |
|---------------|-------------|
| `PostSharpSearchPath` | A semicolon-separated list of directories added to the PostSharp search path. PostSharp will probe these directories when looking for an assembly or an add-in. Note that several directories are automatically added to the search path: the .NET Framework reference directory, the directories containing the dependencies of your project and the directories added to the reference path of your project (tab **Reference Path** in the Visual Studio project properties dialog box).  |
| `SkipPostSharp` | `True` if PostSharp should not be executed.  |
| `PostSharpDisabledMessages` | Comma-separated list of warnings and messages that should be ignored. |
| `PostSharpEscalatedMessages` | Comma-separated list of warnings that should be escalated to errors. Use `*` to escalate all warnings.  |
| `PostSharpLicense` | License key or URL of the license server. |
| `PostSharpProperties` | Additional properties passed to the PostSharp project, in format `Name1=Value1;Name2=Value2`. See <xref:configuration-postsharp>.  |
| `PostSharpConstraintVerificationEnabled` | Determines whether verification of architecture constraints is enabled. The default value is `True`.  |


## Hosting Properties

PostSharp both reads and executes assemblies it transforms. It needs to run under a runtime that allows execution of the input assembly and of it's dependencies. This is done automatically based on target framework of the currently built project. Following properties allow influencing the default behavior.

| Property Name | Description |
|---------------|-------------|
| `PostSharpUsePipeServer` | Specifies whether PostSharp should use a background process invoked synchronously from MSBuild. Valid values are `True` and `False`. Using the pipe server results in lower build time, since PostSharp would otherwise have to be started every time a project is built. The pipe server uses native code and the CLR Hosting API to control the way assemblies are loaded in application domains; the assembly loading algorithm is generally more accurate and predictable than with the managed host. <br>This property is valid only when targetting .NET Framework, otherwise it is ignored. |
| `PostSharpTargetProcessor` | Processor architecture of the PostSharp hosting process. Valid values are `x86` and `x64`. Since PostSharp needs to execute the current project during the build, the processor architecture of the PostSharp process must be compatible with the target platform of the current project. The default value is `x86`, or `x64` if the target platform of the current project is `x64`. <br>This property is valid only when targetting .NET Framework, otherwise it is ignored. |
| `PostSharpBuild` | Build configuration of PostSharp. Valid values are `Release` and `Debug`. Only the `Release` build is distributed in the normal PostSharp packages.  |
| `PostSharpHostConfigurationFile` | Item group configuration files containing assembly binding redirections that should be taken into account by the PostSharp hosting process. By default includes `app.config` and `web.config`.  |
| `PostSharpDependencyRestoreDisabled` | Specifies whether PostSharp should disable restore of it's build time dependencies. Valid values are `True` and `False`. <br>PostSharp's .NET Core process requires it's build-time dependency NuGet packages to be present on the machine. By default it uses NuGet restore to download all packages into a fallback directory. You can use this property to disable this process, at which point PostSharp will expect all dependencies to be present in the NuGet package cache directory used to restore the project being built. You can find the list of dependencies in the verbose build log.<br>This property is valid only when targeting .NET Standard and .NET Core, otherwise it is ignored. |
| `PostSharpReadyToRunDisabled` | Specifies whether PostSharp should try to generate and use ReadyToRun images of itself. Valid values are `True` and `False`, the default value is `False`. <br>When build is running under .NET Core SDK 3.1 and later, PostSharp .NET Core process will attempt to generate and use ReadyToRun images to improve build-time performance. If this generations of these images fails, multiple build warnings may appear in the build log.<br>This property is valid only when targeting .NET Standard and .NET Core, otherwise it is ignored. |


## Diagnostic Properties

| Property Name | Description |
|---------------|-------------|
| `PostSharpAttachDebugger` | If this property is set to `True`, PostSharp will break before starting execution, allowing you to attach a debugger to the PostSharp process. The default value is `False`. For details, see <xref:debugging-buildtime>. <br>When targeting .NET Framework, PostSharp will invoke `System.Diagnostics.Debugger.Launch()`. When targeting .NET Core or .NET Standard, PostSharp will print it's current process ID into the build log and wait for the debugger to be attached.  |
| `PostSharpTrace` | A semicolon-separated list of trace categories to be enabled. Most commonly used categories are `AssemblyBinder`, `PlatformContext`, `Domain`, `ReflectionBinding` and `ProjectLoader`. In order too see the trace output in the build log, at least `normal` verbosity should be set.  |
| `PostSharpBenchmark` | A semicolon-separated list of benchmark categories to be enabled or `all` for all benchmarks to be enabled.  |
| `PostSharpBenchmarkOutputFile` | An absolute path to CSV file where benchmark data should be appended. If the file does not exist, it is going to be created including header line. This is useful when benchmarking multiple projects at once or in automated builds. |
| `PostSharpExpectedMessages` | A semicolon-separated list of codes of expected messages. PostSharp will return a failure code if any expected message was not emitted. This property is used in unit tests of aspects, to ensure that the application of an aspect results in the expected error message. |
| `PostSharpIgnoreError` | If this property is set to `True`, the `PostSharp` MSBuild task will succeed even if PostSharp returns an error code, allowing the build process to continue. The project or targets file can check the value of the `ExitCode` output property of the `PostSharp` MSBuild task to take action.  |
| `PostSharpFailOnUnexpectedMessage` | This property should be used jointly with `PostSharpExpectedMessages`. If it set to `True`, PostSharp will fail if any unexpected message was emitted, even if this message was not an error. This property is used in unit tests of aspects, to ensure that the application of an aspect did not result in other messages than expected.  |


## Directory Properties

PostSharp uses multiple directories during build-time. You can use following properties to override default locations.
| Property Name | Description |
|---------------|-------------|
| `PostSharpBinaryDirectory` | Location of the PostSharp binaries. PostSharp extracts it's binaries into one subdirectory of this directory per PostSharp version.<br>Default on Windows is `%PROGRAMDATA%\PostSharp` (usually `C:\ProgramData\PostSharp`). Default on UNIX systems is `/var/tmp/postsharp`.  |
| `PostSharpCacheDirectory` | Location of the PostSharp caches. PostSharp uses multiple build-time caches shared between PostSharp versions to improve it's performance.<br>Default on Windows is `%PROGRAMDATA%\PostSharp` (usually `C:\ProgramData\PostSharp`). Default on UNIX systems is `/var/tmp/postsharp/cache`.  |
| `PostSharpDependencyDirectory` | Location of the PostSharp dependency directory. PostSharp uses this directory to store it's build time dependencies, usually shared between versions.<br>Default on Windows is `%PROGRAMDATA%\PostSharp\NuGetFallback` (usually `C:\ProgramData\PostSharp\NuGetFallback`). Default on UNIX systems is `/var/tmp/postsharp/NuGetFallback`.  |
| `PostSharpTempDirectory` | Location of the PostSharp temp directory. PostSharp uses this directory to store files that are of temporary nature and does not have to be persisted.<br>Default on Windows is `%TEMP%\PostSharp`. Default on UNIX systems is `/var/tmp/postsharp/temp`.  |


## Other Properties

| Property Name | Description |
|---------------|-------------|
| `PostSharpProject` | Location of the PostSharp project (**.psproj*) to be executed by PostSharp, or the string `None` to specify that PostSharp should not be invoked. If this property is defined, the standard detection mechanism based on references to the *PostSharp.dll* is disabled.  |
| `PostSharpUseHardLink` | Use hard links instead of file copies when creating the snapshot for Visual Studio Code Analysis (FxCop). This property is `True` by default.  |
| `ExecuteCodeAnalysisOnPostSharpOutput` | When set to `True`, executes Microsoft Code Analysis on the *output* of PostSharp. By default, the analysis is done on the *input* of PostSharp, i.e. on the output of the compiler. This property has no effect when Microsoft Code Analysis is disabled for the current build.  |
| `PostSharpCopyCodeAnalysisDependenciesDisabled` | When set to `True`, PostSharp will not copy the all dependencies of the current project output into the *obj\Debug\Before-PostSharp* directory, which contains the copy of the assembly on which Microsoft Code Analysis is executed by default. This property has no effect when Microsoft Code Analysis is disabled for the current build or when the `ExecuteCodeAnalysisOnPostSharpOutput` property has been set to `True`.  |

