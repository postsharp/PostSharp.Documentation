---
uid: semantic-advising
title: "Semantic Advising of Iterator and Async Methods"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses semantic advising in PostSharp for async and iterator methods in C# and VB. It explains how to enable and disable semantic advising and how to handle situations where semantic advising is not available."
---
# Semantic Advising of Iterator and Async Methods

In most C# and VB methods, the source code is very similar to the way how the method is actually executed by the .NET runtime. However, with `async` and iterator methods, mapping between source code and assembly code is far from being straightforward. The compiler performs a complex transformation of the source code and generates a state machine type. If you disassemble an `async` or iterator method, you would just find some instructions that instantiate this state machine. When you apply an aspect to an `async` or iterator method, it leads to an ambiguity whether the aspect should be applied *semantically* at the abstraction level of the source code, or whether it should be applied *non-semantically* at the abstraction level of the assembly code. 

By default, PostSharp applies *semantic advising* for `async` and iterator methods. It also uses semantic advising for all methods returning a <xref:System.Threading.Tasks.Task>. 

This article discusses all details of semantic advising.


## Semantic advising for asynchronous code

Consider the following code snippet:

```cs
public class FlowerService
{
  [MyAspect]
  public Task<Flower> GetFlowerAsync1( int flowerId, string connectionString )
  {
    var connection = ConnectionManager.GetConnection( connectionString );
    return this.GetFlowerAsync2( flowerId, connection );
  }

  [MyAspect]
  public async Task<Flower> GetFlowerAsync2( int flowerId, Connection connection )
  {
      var flowerData = await connection.GetFlowerAsync( flowerId );
      var familyData = await connection.GetFlowerFamilyAsync( flowerData.FamilyId );
      return new Flower( flowerId, flowerData.Name, familyData.Name );
  }
}
```

The `GetFlowerAsync1` method returns a <xref:System.Threading.Tasks.Task> but is not `async` (it would be useless and hurt performance to make it `async`). The `GetFlowerAsync2` method both returns a <xref:System.Threading.Tasks.Task> and is `async`. 

Both methods are enhanced by `[MyAspect]`. For different behaviors implemented by `MyAspect`, how would you expect `MyAspect` to work? 

* If `MyAspect` was an exception handler, you would probably expect all exceptions to be caught by `MyAspect`, including exceptions thrown by the `GetFlowerAsync` and `GetFlowerFamilyAsync` methods and the constructor of the `Flower` class. You would be deceived to realize that the aspect only handles exceptions thrown in the process of instantiating `Task<Flower>` and execute the part of the task that can run synchronously. 

* If `MyAspect` was a profiling aspect, you would probably want to measure the time taken by the whole method to execute. That is, you will probably be interested in the time of the whole `Task<Flower>` to run to completion, not just the time to instantiate it and run to the first waiting point. 

* If `MyAspect` was a caching aspect, you would probably want to cache the `Flower` object, not the `Task<Flower>` itself. 

That is, most of the time, you want the aspect to apply to the *semantic* of the method, not to its *implementation* (how it is implemented in MSIL and executed by the .NET runtime). 

We use the term *semantic advising* when an aspect or advice is applied to the level of abstraction of the programming language (C# or VB). Non-semantic advising or low-level advising means that PostSharp applies the aspect to the level of abstraction of MSIL. 

Semantic advising is the default behavior for all methods returning a <xref:System.Threading.Tasks.Task> (or any other awaitable type such as `ValueTask`) and all `async` methods. 


## Synchronization context

In method boundary aspects, all advices are executed in the synchronization context of the caller of the advised method. For example, if you await an `async` method from the event handler of an event in a Windows Forms application, all advices (such as <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)> and <xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnExit(PostSharp.Aspects.MethodExecutionArgs)>) are also executed on the Windows Forms UI thread. 

It is generally a good choice to execute the advices in the current synchronization context. However, it can cause a deadlock in a situation where the synchronization context is blocked until the advised method completes.

Example of the deadlock:

```cs
[OnMethodBoundaryAspect1]
Task Return4()
{
  return Task.Delay(500);
} 
void button1_Clicked(object sender, EventArgs args)
{
  Return4().Wait(); // blocks the Windows Forms UI thread
  // without PostSharp, the thread would get unblocked when the delay completes
  // with PostSharp, there is a deadlock because OnMethodBoundaryAspect1's OnExit advice needs to happen in the Windows Forms UI thread
}
```

If you do not want to execute advices in the current synchronization context, use <xref:PostSharp.Aspects.MethodInterceptionAspect> instead of <xref:PostSharp.Aspects.OnMethodBoundaryAspect>. The interception aspect gives you more control over the synchronization context. 


## Semantic advising for iterators

The notion of semantic advising also applies to iterator methods, i.e. methods that include the `yield return` statement. 

Consider the following code snippet:

```cs
public class PostcardService
{

  [MyAspect]
  public IEnumerable<Postcard> GetPostcards1( )
  {
      return new [] { new Postcard("Hello from Alaska"), new Postcard("Hello from Siberia") };
  }

  [MyAspect]
  public IEnumerable<Postcard> GetPostcards2( int flowerId, Connection connection )
  {
      yield return new Postcard("Hello from Alaska");
      yield return new Postcard("Hello from Siberia");
  }

}
```

The `GetPostcards2` method requires special attention. Under the hood, the C# or VB compiler generates a new class implementing the <xref:System.Collections.Generic.IEnumerable`1> and <xref:System.Collections.Generic.IEnumerator`1> interfaces, called the enumerator class. At runtime, calling the `GetPostcards2` method only instantiates the enumerator class. The initial logic of `GetPostcards2` is moved to the `MoveNext` method of the enumerator. 

Let's do the same exercise as for asynchronous methods. For different behaviors implemented by `MyAspect`, how would you expect `MyAspect` to work? 

* If `MyAspect` was an exception handler, you would probably want the aspect to catch any exception thrown by the C# code that you can see. That is, in `GetPostcards2`, you actually want to catch exceptions in the `MoveNext` method of the enumerator class. In this case, you need semantic advising. 

* If `MyAspect` was a profiling aspect, you may want to only measure the time when `GetPostcards2` is actually executing, but exclude the time when the caller is processing the data returned by the enumerator. Therefore, you will also want to add behaviors to the `MoveNext` method of the enumerator class. In this case again, you need semantic advising. 

* If `MyAspect` was a caching aspect, however, you will want to cache a copy of the enumerator itself, therefore you will need to enhance the `GetPostcards2` method and not the `MoveNext` method. In this case, you don't need semantic advising. 


## Semantic advising vs non-semantic advising

The following table compares semantic advising with non-semantic advising in several situations.

|  | Semantic Advising | Non-Semantic Advising |
|--|-------------------|-----------------------|
| **Async methods:** |  |  |
| Code covered or intercepted by the aspect | The whole async method. | The part of the async method before the first `await` operator whose operand (typically a <xref:System.Threading.Tasks.Task>) has not yet completed.  |
| Return value | The operand of the `return` statement, i.e. <xref:System.Threading.Tasks.Task`1.Result>.  | The <xref:System.Threading.Tasks.Task`1> object itself.  |
| **Non-async methods returning a  T:System.Threading.Tasks.Task:** |  |  |
| Code covered or intercepted by the aspect | Both the code that instantiates or gets the <xref:System.Threading.Tasks.Task> and the whole execution of the <xref:System.Threading.Tasks.Task>.  | The compiler-generated code that instantiates or gets the <xref:System.Threading.Tasks.Task> (and if the <xref:System.Threading.Tasks.Task> represents an `async` method, plus the first segment of the method that runs synchronously).  |
| Return value | The value of the <xref:System.Threading.Tasks.Task`1.Result> property.  | The <xref:System.Threading.Tasks.Task`1> object itself.  |
| **Iterator methods:** |  |  |
| Code covered or intercepted by the aspect | The whole method. | The compiler-generated code that instantiates the enumerator class (no user code is covered). |
| Return value | None. | The enumerator. |
| **Non-iterator methods returning ``T:System.Collections.Generic.IEnumerable`1``:** |  |  |
| Code covered or intercepted by the aspect | The returned enumerator's `MoveNext` method.  | The method that is being enhanced by the aspect. |
| Return value | None. | The enumerator or enumerable object. |
| **Async iterator methods:** |  |  |
| Code covered or intercepted by the aspect | (not supported) | The compiler-generated code that instantiates the async enumerator class (no user code is covered). |
| Return value | (not supported) | The async enumerator. |


## Supported and default advising modes

Semantic advising is available for the <xref:PostSharp.Aspects.OnMethodBoundaryAspect>, <xref:PostSharp.Aspects.OnExceptionAspect> and <xref:PostSharp.Aspects.MethodInterceptionAspect> aspects. Whenever semantic advising makes sense, it is the default advising mode, except for normal methods returning <xref:System.Collections.Generic.IEnumerable`1> or <xref:System.Collections.Generic.IEnumerator`1> which are not semantically advised by default because we expect that non-semantic advising is more often what you need for those methods. 



The following table specifies where semantic advising is supported and where it is the default advising mode.

| Target methods | Advising modes supported by OnMethodBoundaryAspect and OnExceptionAspect | Advising modes supported by MethodInterceptionAspect | Default advising mode |
|----------------|--------------------------------------------------------------------------|------------------------------------------------------|-----------------------|
| `async` method returning `void`  | Semantic and non-semantic | Non-semantic | Semantic |
| `async` method returning a <xref:System.Threading.Tasks.Task>  | Semantic and non-semantic | Semantic and non-semantic | Semantic |
| `async` method returning something else than `void` or a <xref:System.Threading.Tasks.Task>  | Semantic and non-semantic | Non-semantic | Semantic |
| Iterator method | Semantic and non-semantic | Non-semantic | Semantic |
| Normal method returning <xref:System.Collections.Generic.IEnumerable`1> or <xref:System.Collections.Generic.IEnumerator`1>  | Semantic and non-semantic | Non-semantic | Non-semantic |
| Async iterator method (C# 8) | Non-semantic | Non-semantic | Semantic |

> [!WARNING]
> With <xref:PostSharp.Aspects.MethodInterceptionAspect>, the default advising mode is *always* non-semantic when the <xref:PostSharp.Aspects.MethodInterceptionAspect.OnInvokeAsync(PostSharp.Aspects.MethodInterceptionArgs)> advice is not implemented by the aspect. 

The default mode is always semantic, even in situations where semantic advising is not available. This design allows us to implement support for semantic advising in future versions of PostSharp without breaking backward compatibility. However, PostSharp will emit a build-time error if you try to use semantic advising on a method that is not supported.

The next sections explain how to opt out from semantic advising and how to cope with situations when semantic advising is not available.


## Enabling and disabling semantic advising

You can disable or enable semantic advising by setting the <xref:PostSharp.Aspects.OnMethodBoundaryAspect.SemanticallyAdvisedMethodKinds> property of your aspect. You would typically set this property in the constructor or in the <xref:PostSharp.Aspects.MethodLevelAspect.CompileTimeInitialize(System.Reflection.MethodBase,PostSharp.Aspects.AspectInfo)> method. 

If you want to disable semantic advising in all situations, set the <xref:PostSharp.Aspects.OnMethodBoundaryAspect.SemanticallyAdvisedMethodKinds> property to `None`. Otherwise, you can select individual situations in which semantic advising should be applied by setting the property to a bitwise combination of the values of the <xref:PostSharp.Aspects.SemanticallyAdvisedMethodKinds> enumeration. For instance, the `Async | Iterator` value instructs PostSharp to use semantic advising for `async` methods and iterators but not for other methods returning a <xref:System.Threading.Tasks.Task> or an enumerable. 


### Example

The following code snippet shows how to configure a caching aspect so that semantic advising is used for methods returning a <xref:System.Threading.Tasks.Task> but not for methods returning an enumerable. 

```csharp
[PSerializable]
    public class CacheAttribute : MethodInterceptionAspect
    {
    
      public CacheAttribute()
      {
          this.SemanticallyAdvisedMethodKinds = SemanticallyAdvisedMethodKinds.ReturnsAwaitable;
      }
      
      // Detailed skipped.
    
    }
```


## Coping with situations where semantic advising is not available

By default, PostSharp will emit a build-time error if you're applying a semantically advising aspect to a method that does not support it (for instance, an asynchronous <xref:PostSharp.Aspects.MethodInterceptionAspect> cannot be applied to an `async void` method). 

Instead of failing with an error, you can change the behavior by setting the <xref:PostSharp.Aspects.OnMethodBoundaryAspect.UnsupportedTargetAction> aspect property. The default value is `Fail`. You can choose `Ignore` to silently skip applying the aspect or advice to the target method, or `Fallback` to apply non-semantic advising. 

If you are using composite aspects, you can change the attribute property <xref:PostSharp.Aspects.Advices.OnMethodBoundaryAdvice.UnsupportedTargetAction> and similarly named properties on other advices. 

## See Also

**Reference**

<xref:PostSharp.Aspects.OnMethodBoundaryAspect>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnYield(PostSharp.Aspects.MethodExecutionArgs)>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnResume(PostSharp.Aspects.MethodExecutionArgs)>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnExit(PostSharp.Aspects.MethodExecutionArgs)>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.OnSuccess(PostSharp.Aspects.MethodExecutionArgs)>
<br><xref:PostSharp.Aspects.MethodExecutionArgs>
<br><xref:PostSharp.Aspects.OnMethodBoundaryAspect.SemanticallyAdvisedMethodKinds>
<br><xref:PostSharp.Aspects.MethodExecutionArgs.MethodExecutionTag>
<br><xref:PostSharp.Serialization.PSerializableAttribute>
<br>**Other Resources**

<xref:method-decorator>
<br>[PostSharp Aspect Framework - Product Page](https://www.postsharp.net/aspects)
<br>
