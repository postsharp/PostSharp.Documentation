---
uid: debugging-buildtime
title: "Debugging Build-Time Logic"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a guide on how to debug build-time logic using PostSharp, by attaching a debugger to the PostSharp process and using Visual Studio to set breakpoints."
---
# Debugging Build-Time Logic

It may seem unusual to debug compile-time logic, but like any process, it is perfectly legal and even simple to debug the build process!

Basically, what you will do is to attach a debugger to the PostSharp process. If you use the standard MSBuild targets for PostSharp, define the constant `PostSharpAttachDebugger=True`. 

The trick is easier to explain when you have compile-time logic (your aspect, for instance) and the transformed assembly in different Visual Studio projects.

Suppose you have your aspects logic `MyAspects.csproj` and unit tests (i.e. the code to be transformed) in `MyAspects.Test.csproj`. The easiest way to debug `MyAspects.csproj` is the following: 


### To debug the build-time logic of an aspect:

1. Open Visual Studio and load the solution containing `MyAspects.csproj`. 


2. Open the Visual Studio Command Prompt and go to the directory containing `MyAspects.Test.csproj`. 


3. Build `MyAspects.csproj` using Visual Studio as usual . 


4. From the command prompt, type:

    <code>msbuild *MyAspects.Test.csproj* /T:Rebuild /P:PostSharpAttachDebugger=True </code>



5. The build process will hit a break point. When it happens, attach the instance of `MyAspects.csproj` Visual Studio. 

    > [!NOTE]
    > Because of a bug in Visual Studio, you need to use the **mixed debugging engine**. To do that, check the option **Manually choose the debugging engines** in the Visual Studio Just-In-Time Debugger and select both the managed and the native engines. 


6. Set up break points in your code and continue the program execution.



