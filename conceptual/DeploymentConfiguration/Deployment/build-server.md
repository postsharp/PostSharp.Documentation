---
uid: build-server
title: "Using PostSharp on a Build Server"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses how to use PostSharp on a build server, emphasizing its ease of use and various ways to install a license on the server."
---
# Using PostSharp on a Build Server

PostSharp has been designed for frictionless use on build servers. PostSharp build-time components are deployed as NuGet packages, and are integrated with MSBuild. No component needs to be installed or configured on the build server, and no extra build step is necessary. If you choose not to check in NuGet packages in your source control, read <xref:nuget-restore>. 


## Installing a License on the Build Server

There are several ways to install a license on the build server:

* Don't install it. It is not necessary to install the license key on the build server unless you are using the features of PostSharp Logging.

* Add your license key to the *postsharp.config* file and add this file to the source repository as described in <xref:deploying-keys>. 

* Set an environment variable named `PostSharpLicense` to a semicolon-separated list of your license keys. 

We do not recommend to install the license key on a build server using the user interface.

## See Also

**Other Resources**

<xref:installation>
<br><xref:deploying-keys>
<br>
