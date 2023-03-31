---
uid: whats-new-43
title: "What's New in PostSharp 4.3"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# What's New in PostSharp 4.3

The objective of PostSharp 4.3 is to address the most important concerns of current PostSharp customers. We focused on improving the existing features without adding brand new ones.


## Improved build-time performance

We understand that nobody likes to wait for the build to complete, so we've been working hard to optimize PostSharp's build performance. PostSharp 4.3 is up to 1.5 times faster than PostSharp 4.2 (the improvement is especially visible in large projects) and you don't need to change anything in your code.

But there's more. PostSharp 4.3 introduces a new feature called *solution-wide build optimizations*, which can double the build speed in large solutions. Since this feature can break custom build-time logic, it is disabled by default. (This feature has been deprecated.) 


## Improved debugging experience

Debugging an application enhanced with aspects is now even easier thanks to the following improvements:

* Full support for Just My Code.

* During Step Into, aspect code is now stepped over by default.

* The call stack no longer contains PostSharp implementation details by default.

To learn more about the new debugging behaviors and how to disable them, see <xref:debugging-runtime>. 


## More flexible deployment

PostSharp 4.3 brings more freedom when it comes to deployment and installation:

* **Alternative to NuGet.** Between versions 3.0 and 4.2, the PostSharp compiler and libraries were only distributed as NuGet packages. Starting from version 4.3, we are re-introducing the old good zip file, and integrate it better with PostSharp Tools for Visual Studio. See <xref:install-without-nuget> for details. 

* **Command Line Tool.** Using PostSharp as a command-line tool is now a supported and documented scenario. This feature has been discontinued in PostSharp 5.0. 

* **PostSharp Tools for Visual Studio no longer required.** You will now be able to build a project that uses PostSharp without having PostSharp Tools installed in Visual Studio. The tooling is still highly recommended but no longer strictly required. 


## Improvements in the NotifyPropertyChanged aspect

PostSharp 4.3 brings two improvements to the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect: 

* Support for Caliburn.Micro and MVVM Light. See <xref:inotifypropertychanged-frameworks>. 

* Option to avoid false positives. See <xref:inotifypropertychanged-false-positives> for details. 


## Simplified licensing of PostSharp Express

The limitations of PostSharp Express, the free edition of PostSharp, are now clearer and easier to understand and remember. For details, see <xref:express-limitations>. 


## Source code sharing with non-licensed teams

You no longer need a PostSharp license to build code that someone else wrote and uses PostSharp. A license is only required for code that you build or edited yourself. See <xref:licensing-shared-source-code> for details. 


## Automatic computing of build-time assembly binding redirections

It is no longer necessary to manually create an assembly binding redirection file for PostSharp. For details, see <xref:assembly-binding-resolution>. 

