---
uid: getting-started-development
title: "Developer Role: Using Aspects"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a guide for developers on using PostSharp aspects, including how to install and upgrade PostSharp, work with pre-built patterns, and work with patterns."
---
# Developer Role: Using Aspects

Using aspects requires much less training than creating new ones. In typical large teams, only a few developers or architects develop new aspects, while the rest of the team uses existing aspects. This section focuses on the skill set that you need to acquire if you have to be able to use PostSharp aspects but don't need to create your own.

In this session, we also assume that PostSharp has been properly deployed into your development and build environments.

> [!NOTE]
> Of course, you can learn PostSharp as much as you want. The role of this section is to provide a short list of articles to minimize your learning curve and get you productive as quickly as possible, but this should not stop you from learning and experimenting more.


## Installing and upgrading PostSharp

Every process has a starting point. Learn how to add PostSharp to your project so that you can get started with improving your codebase.

| Topic | Articles |
|-------|----------|
| Install PostSharp to your machine | <xref:requirements><br><xref:installation> |
| Add PostSharp to a project and keep it up-to-date | <xref:install-compiler><br><xref:upgrade> |


## Working with pre-built patterns

PostSharp offers a number of different pre-built patterns. You will need to learn those that will be used in your application.

| Topic | Articles |
|-------|----------|
| Diagnostics | <xref:logging> |
| Code Contracts | <xref:contracts> |
| INotifyPropertyChanged | <xref:inotifypropertychanged> |
| Aggregatable | <xref:aggregatable> |
| Disposable | <xref:disposable> |
| Threading Models | <xref:threading-models><br><xref:freezable>, <xref:immutable>, <xref:actor>, <xref:reader-writer-synchronized>, <xref:synchronized>, <xref:thread-unsafe>, <xref:thread-affine> <br><xref:threading-model-compatibility><br><xref:threading-waiving-verification> |
| Dispatching Threads | <xref:ui-dispatching><br><xref:background-dispatching> |
| Architecture Validation | <xref:control-implementation><br><xref:control-visibility> |


## Working with Patterns

The following resources are for all aspects. You can save a great amount of time in learning to master them.

| Topic | Articles |
|-------|----------|
| Adding aspects to several declarations | <xref:multicast> |
| Resolving Errors | <xref:messages> |


