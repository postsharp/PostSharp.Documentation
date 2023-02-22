---
uid: how-it-works
title: "How Does PostSharp Work"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# How Does PostSharp Work

On a conceptual level, you can think of PostSharp as an extension to the C# or VB compiler. Practically, Microsoft's compilers themselves are not extensible, but the build process can be easily extended. That's exactly what PostSharp is doing: it inserts itself into the build process and post-processes the output of the compiler.


## MSBuild Integration

PostSharp integrates itself in the build process thanks to *PostSharp.targets*, which is imported into each project using PostSharp by the NuGet installation script *install.ps1*. *PostSharp.targets* adds a few steps to the build process. The principal step is the post-processing of the compiler's output by PostSharp itself. 

See <xref:configuration-msbuild> for details. 


## MSIL Rewriting

PostSharp post-processes the compiler output by reading and disassembling the intermediate assembly, executing the required transformations and validations, and writing the final assembly back to disk.

Although this might sound magic or dangerous, PostSharp's MSIL technology is stable and mature, and has been used by tens of thousands of projects since 2004. Other .NET products relying on MSIL transformation or analysis include Microsoft Code Contracts, Microsoft Code Analysis, and Microsoft Code Coverage.

