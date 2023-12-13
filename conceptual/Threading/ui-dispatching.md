---
uid: ui-dispatching
title: "Dispatching a Method to the UI Thread"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Dispatching a Method to the UI Thread

When you are building desktop or mobile user interfaces, parts of your code may execute on background threads. However, the user interface itself can be accessed only from the UI thread. Therefore, it is often necessary to dispatch execution of code from a background thread to the foreground thread.

Traditionally, thread dispatching has been implemented using the <xref:System.Windows.Forms.Control.Invoke(System.Delegate)> method in WinForms or the <xref:System.Windows.Threading.Dispatcher> class in XAML. However, this results in a large amount of boilerplate, making the code unreadable. 

The <xref:PostSharp.Patterns.Threading.DispatchedAttribute> aspect addresses the issue of thread dispatching by forcing a method to execute on the thread that created the object (typically the foreground thread). 


## Forcing a method to execute on the foreground thread


### To force a method to execute on the foreground thread:

1. Add the `PostSharp.Patterns.Threading` package to your project. 


2. Add `using PostSharp.Patterns.Threading` namespace to your file. 


3. Add the <xref:PostSharp.Patterns.Threading.DispatchedAttribute> to the method that you want to be executed on the foreground thread, regardless of where it's called from. 



### Example

The following example shows how to use both <xref:PostSharp.Patterns.Threading.BackgroundAttribute> and <xref:PostSharp.Patterns.Threading.DispatchedAttribute> to specify on which threads different methods of the class are executed. 

```csharp
[Background]
private void SaveButton_Click( object sender, RoutedEventArgs e )
{
   using ( var file = File.CreateText( this.path ) )
   {
      this.model.SaveTo( file );
   }
   
   this.SaveCompleted();
}

[Dispatched] 
private void SaveCompleted() 
{ 
    this.StatusLabel.Text = "Finished Saving"; 
}
```


## Executing a method asynchronously

By default, the <xref:PostSharp.Patterns.Threading.DispatchedAttribute> forces the target method to execute synchronously on the foreground thread, which means that the background thread will wait until the method execution has completed. This waiting causes some performance overhead. Additionally, synchronous execution is not always useful. If the method has no return value and no side effect of interest for the calling thread, the method could be safely executed asynchronously, which means the calling thread would not need to wait for the method execution to complete on the foreground thread, so that the calling thread would continue its execution immediately after having enqueued the call to the foreground thread. 

You can enable asynchronous execution of a dispatched method by passing the `true` value to the parameter of the <xref:PostSharp.Patterns.Threading.DispatchedAttribute> class constructor, for instance: 

```csharp
[Dispatched(true)] 
private void SaveCompleted() 
{ 
    this.StatusLabel.Text = "Finished Saving"; 
}
```


## Executing async methods in the foreground thread

When you use the <xref:PostSharp.Patterns.Threading.DispatchedAttribute> aspect on asynchronous methods (`async` keyword in C#), the method is guaranteed to execute on the foreground thread even when it is invoked from a background thread. 

## See Also

**Reference**

<xref:PostSharp.Patterns.Threading.DispatchedAttribute>
<br><xref:PostSharp.Patterns.Threading.BackgroundAttribute>
<br>**Other Resources**

<xref:background-dispatching>
<br>