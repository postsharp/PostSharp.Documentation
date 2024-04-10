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

* CodeLens now displays details of how PostSharp enhances your code. This feature, previously available for Metalama in its former extension, has now been introduced to PostSharp. CodeLens replaces the previous mechanism based on code underlining and tooltips. If you miss the old mechanism, it can still be enabled in the Visual Studio Tools for Metalama and PostSharp options.

* The Visual Studio extension no longer packaged as an `.exe` installer file but, as most extensions, as a single `.vsix` file that will work with all versions of Visual Studio. The principal benefit of this change is to allow for automatic updates of the extension.




* The extension is no longer released alongside each PostSharp version. Starting from Metalama 2024.1, it is released with each Metalama version instead. The PostSharp Tools for Visual Studio have been backward compatible with previous versions of PostSharp, and this will continue with the new Visual Studio Tools for Metalama and PostSharp.

## Licensing

* Preview builds of PostSharp now require a license key, except during the 45-day evaluation period.
