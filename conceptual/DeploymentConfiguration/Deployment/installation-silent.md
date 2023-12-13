---
uid: installation-silent
title: "Installing PostSharp Tools for Visual Studio Silently"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides instructions on how to silently install PostSharp Tools for Visual Studio, a user interface and build component tool, through a command line interface."
---
# Installing PostSharp Tools for Visual Studio Silently

PostSharp is composed of a user interface (PostSharp Tools for Visual Studio) and build components (NuGet packages). NuGet packages are usually checked into source control or retrieved from a package repository at build time (see <xref:nuget-restore>), so its deployment does not require additional automation. The user interface is typically installed by each user. It does not require administrative privileges. 

In large teams, it might be inconvenient to install PostSharp Tools for Visual Studio on each machine manually. For this purpose, PostSharp installer enables silent installation using a command line interface. You can install PostSharp automatically for a large number of users using the silent installer.


### To install PostSharp unattended:

1. Download the installer from [https://www.postsharp.net/download](https://www.postsharp.net/download). The installer is a file named *PostSharp-X.X.X.exe*. 


2. Extract the installer files using the following command line:

    ```none
    PostSharp-X.X.X.exe /extract %TEMP%\PostSharp.X.X.X
    ```


3. Execute the following command line:

    ```none
    %TEMP%\PostSharp.X.X.X\PostSharp.Settings.exe /setup
    ```

    In the command line above, the following arguments are optional but recommended:

    | Argument | Description |
    |----------|-------------|
    | `/ceip` | Allows PostSharp to collect anonymous information about the way you are using the software. We never collect your source code. See our [privacy policy](https://www.postsharp.net/company/legal/privacy-policy#ceip) for details. <br>If you don't specify this flag, CEIP will be disabled, and the user will not be asked to enable it. |
    | `/license 000-AAAAAAAAAAAAAAA` | Installs the license key for the current user in registry. In this argument, *000-AAAAAAAAAAAAAAA* must be replaced by the license key or the URL to the license server.  |
    | `/license-all 000-AAAAAAAAAAAAAAA` | Installs the license key for all users in registry. In this argument, *000-AAAAAAAAAAAAAAA* must be replaced by the license key or the URL to the license server.  |

    > [!WARNING]
    > If the `/license` and `/license-all` arguments are omitted, the license keys on the machine will remain unchanged or uninstalled. 


> [!NOTE]
> If any Visual Studio instance or other processes affected by the installer is running during the installation process, the installer lists all the blocking processes, and fails. You need to run silent installation when none of these processes is running.

## See Also

**Other Resources**

<xref:install-compiler>
<br><xref:uninstalling>
<br>
