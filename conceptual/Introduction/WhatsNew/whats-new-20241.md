---
uid: whats-new-20241
title: "What's New in PostSharp 2024.1"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp 2024.1 introduces a consolidated Visual Studio extension for Metalama and PostSharp, along with support for CodeLens."
---
# What's New in PostSharp 2024.1

PostSharp 2024.1 introduces a consolidated Visual Studio extension for Metalama and PostSharp, along with support for CodeLens.

## Visual Studio Tools for Metalama and PostSharp

* The Visual Studio extension for PostSharp has been consolidated with the extension for Metalama. Now renamed as [Visual Studio Tools for Metalama and PostSharp](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.PostSharp), this single extension can be used to work with both [Metalama](https://www.postsharp.net/metalama) and/or [PostSharp](https://www.postsharp.net/il).

* CodeLens now displays details of how PostSharp enhances your code. This feature, previously available for Metalama in its former extension, has now been introduced to PostSharp. This feature replaces the underlining of enhanced code and related information in tooltips. If you miss this replaced feature, it can still be enabled in the Visual Studio Tools for Metalama and PostSharp options.

* There is no longer a standalone installer for the Visual Studio extension. This change brings several benefits:

  * There is a single VSIX installer for all supported versions of Visual Studio.

  * Automatic updates of the extension can be performed by Visual Studio.

  * Unattended installation of the extension no longer relies on the PostSharp installer.

* The extension is no longer released alongside each PostSharp version. Starting from Metalama 2024.1, it is released with each Metalama version instead. The PostSharp Tools for Visual Studio have been backward compatible with previous versions of PostSharp, and this will continue with the new Visual Studio Tools for Metalama and PostSharp.

## Licensing

* Preview versions of PostSharp no longer offer a trial license. This feature required strong coupling between the PostSharp version and the Visual Studio extension version. Since the Visual Studio extension is no longer tied to a specific version of PostSharp, we had to remove this feature. Now, a valid license is required for any version of PostSharp.
