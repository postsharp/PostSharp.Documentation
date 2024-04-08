---
uid: whats-new-20241
title: "What's New in PostSharp 2024.1"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2024.1 introduces consolidated Visual Studio extension for Metalama and PostSharp and support for CodeLens."
---
# What's New in PostSharp 2024.1

PostSharp 2024.1 introduces consolidated Visual Studio extension for Metalama and PostSharp and support for CodeLens.

## Visual Studio Tools for Metalama and PostSharp

* The Visual Studio extension for PostSharp got consolidated with the extension for Metalama. It is now called [Visual Studio Tools for Metalama and PostSharp](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.PostSharp) and this single extension can be used to work with both [Metalama](https://www.postsharp.net/metalama) and/or [PostSharp](https://www.postsharp.net/il).

* CodeLens show details of how PostSharp enhances your code. This feature has been available for Metalama in it's former extesnion, and now it's been brought to PostSharp. This feature replaces the underlining of enhanced code and related information in tooltips. In case you miss this replaced feature, it can still be enabled in the Visual Studio Tools for Metalama and PostSharp options.

* There is no longer a standalone installer for the Visual Studio extension. This change brings the following benefits:

  * There is a single VSIX installer for all supported Versions of Visual Studio.

  * Automatic updates of the extension can be performed by Visual Studio.

  * Unattended installation of the extension no longer relies on the PostSharp installer.

* The extension is no longer release along with each PostSharp version. Since Metalama 2024.1, it is released along with each Metalama version instead. The PostSharp Tools for Visual Studio have been backward compatible with previous versions of PostSharp, and it's going to stay like that with the new Visual Studio Tools for Metalama and PostSharp.

## Licensing

* Preview versions of PostSharp no longer offer a trial license. This feature required strong coupling between PostSharp version and the Visual Studio extension version. Since the Visual Studio extension is no longer bound to a specific version of PostSharp, we had to remove this feature, and a valid license is now required for any PostSharp version.