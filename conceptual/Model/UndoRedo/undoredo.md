---
uid: undoredo
title: "Undo/Redo"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Undo/Redo

Most business application users are familiar with applications that have the ability to undo and redo changes that they have made. It’s not common to see this functionality in custom built applications because it is quite difficult to do. Despite this difficulty, undo/redo is consistently mentioned on the top of users' wish list.

The <xref:PostSharp.Patterns.Recording.RecordableAttribute> aspect makes it much easier to add undo/redo to your application by automatically appending changes done on your object model to a <xref:PostSharp.Patterns.Recording.Recorder> that you can then bind to your user interface. Unlike other approaches to undo/redo, the <xref:PostSharp.Patterns.Recording.RecordableAttribute> aspect only requires minimal changes to your source code. 


## In this chapter

| Section | Description |
|---------|-------------|
| <xref:undoredo-start> | The first step is to make your model classes recordable. This section shows how to add the <xref:PostSharp.Patterns.Recording.RecordableAttribute> aspect to the model classes to enable the undo/redo functionality.  |
| <xref:undoredo-ui> | This section describes how to expose the undo/redo functionality to the application's users. |
| <xref:undoredo-operation-name> | This section shows how to group changes into logical operations and give them a name that is meaningful to the application's users. |
| <xref:undoredo-recorder> | This section explains how to customize the assignment of recordable objects to recorders. |
| <xref:undoredo-callbacks> | This section shows how to execute custom logic when undo/redo operations occur in a recordable object. |
| <xref:undoredo-conceptual> | This section describes the concepts and architecture of the <xref:PostSharp.Patterns.Recording.RecordableAttribute> aspect.  |

