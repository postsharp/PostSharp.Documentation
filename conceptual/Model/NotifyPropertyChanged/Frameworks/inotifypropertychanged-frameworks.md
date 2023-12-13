---
uid: inotifypropertychanged-frameworks
title: "Integrating the NotifyPropertyChanged Aspect with UI Frameworks"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses how to integrate the NotifyPropertyChanged Aspect from PostSharp with various UI frameworks to simplify the creation of XAML applications."
---
# Integrating the NotifyPropertyChanged Aspect with UI Frameworks

There are frameworks that help you to create XAML applications including binding of objects in a simplified way. But usually, you still need to include some repetitive code anyway.

PostSharp can eliminate most of that repetition for you. All you have to do is make use of the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect. 

In this chapter, you can find examples of using the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect with some of the popular UI frameworks. 

This list of supported frameworks is not exhaustive. If the framework you are using is not listed here, it does not necessarily mean that PostSharp will not work with the framework.


## In this chapter

| Topic | Description |
|-------|-------------|
| <xref:inotifypropertychanged-caliburnmicro> | This section shows how to use the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect with the Caliburn.Micro framework.  |
| <xref:inotifypropertychanged-mvvmlight> | This section shows how to use the <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect with the MVVM Light Toolkit.  |


