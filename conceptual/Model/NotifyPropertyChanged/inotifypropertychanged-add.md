---
uid: inotifypropertychanged-add
title: "Implementing INotifyPropertyChanged"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document provides a guide on how to implement the INotifyPropertyChanged interface using PostSharp, including adding the NotifyPropertyChanged aspect, and accessing the INotifyPropertyChanged interface from your code."
---
# Implementing INotifyPropertyChanged

This section shows how to make your class automatically implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect. 


## Adding the NotifyPropertyChanged aspect


### To add INotifyPropertyChanged aspect:

1. Use NuGet Package Manager to add the *PostSharp.Patterns.Model* package to your project. 


2. Import the <xref:PostSharp.Patterns.Model> namespace into your file. 


3. Add the `[NotifyPropertyChanged]` custom attribute to the class. 


All properties of the class now fire the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event whenever they are changed. You can prevent a property from automatically firing the PropertyChanged event with <xref:PostSharp.Patterns.Model.IgnoreAutoChangeNotificationAttribute>. 

Note that the `[NotifyPropertyChanged]` aspect is inherited. Changes of properties in all derived classes will automatically be notified. 

By using PostSharp to add <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> to your model classes you are able to eliminate most of the repetitive boilerplate coding tasks and code from the codebase. 

> [!NOTE]
> This procedure has added <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> to one class hierarchy. If you need to add the aspect to several classes in your codebase, consider using aspect multicasting. See <xref:attribute-multicasting> for details. We recommend to be careful with multicasting and to add this aspect only to classes that really require the feature because this aspect has some performance and memory consumption overhead. 


### Example

```csharp
[NotifyPropertyChanged] 
public class CustomerForEditing 
{ 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 

    public string FullName  
    {  
        get { return string.Format("{0} {1}", this.FirstName, this.LastName); }  
    } 

}
```


## Consuming the INotifyPropertyChanged interface

Since the <xref:System.ComponentModel.INotifyPropertyChanged> interface is implemented by PostSharp at build time after the compiler has completed, the interface will neither be visible to Intellisense or other tools like Resharper, neither to the compiler. The same is true for the `PropertyChanged` event. 

In many cases, this limitation does not matter because the interface is consumed from a framework (like WPF) that is not coupled with your project. However, in some situations, you may need to access the <xref:System.ComponentModel.INotifyPropertyChanged> interface. 

There are two ways to access the <xref:System.ComponentModel.INotifyPropertyChanged> interface from your code: 

* You can cast your object to <xref:System.ComponentModel.INotifyPropertyChanged>, for instance: 
    ```csharp
    ((INotifyPropertyChanged) obj).PropertyChanged += obj_OnPropertyChanged;
    ```

    If your tooling complains that the object does not implement the interface, you can first cast to `object`: 
    ```csharp
    ((INotifyPropertyChanged) (object) obj).PropertyChanged += obj_OnPropertyChanged;
    ```


* You can use the <xref:PostSharp.Post.Cast``2(``0)> method. The benefit of using this method is that the cast operation is validated by PostSharp, to the build will fail if you try to cast an object that does not implement the <xref:System.ComponentModel.INotifyPropertyChanged> interface. For instance: 
    ```csharp
    Post.Cast<Foo,INotifyPropertyChanged>(obj).PropertyChanged += obj_OnPropertyChanged;
    ```


## See Also

**Reference**

<xref:System.ComponentModel.INotifyPropertyChanged>
<br><xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute>
<br>**Other Resources**

<xref:attribute-multicasting>
<br>
