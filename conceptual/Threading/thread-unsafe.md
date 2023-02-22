---
uid: thread-unsafe
title: "Thread-Unsafe Threading Model"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Thread-Unsafe Threading Model

When you are dealing with multithreaded code you will run into situations where some objects are not safe for concurrent use by several threads. Although these objects should theoretically not be accessed concurrently, it is very hard to prove that it never happens. And when it does happen, thread-unsafe data structures get corrupted, and symptoms may appear much later. These issues are typically very difficult to debug. So instead of relying on hope, it would be nice if the object threw an exception whenever it is accessed simultaneously by several threads. This is why we have the thread-unsafe threading model.


## Applying the Thread-Unsafe model to a class


### To apply the Thread-Unsafe threading model to a class:

1. Add the `PostSharp.Patterns.Threading` package to your project. 


2. Add `using PostSharp.Patterns.Threading` namespace to your file. 


3. Add the <xref:PostSharp.Patterns.Threading.ThreadUnsafeAttribute> to the class. 


4. Annotate your object model for parent/child relationships as described in <xref:aggregatable-adding>. 



## Rules enforced by the Thread-Unsafe aspect

The <xref:PostSharp.Patterns.Threading.ThreadUnsafeAttribute> aspect emits build-time errors in the following situations: 

* The class contains a public or internal field.

Internally, the Thread-Unsafe model is implemented by a lock. The lock is automatically acquired by public and internal methods, just like the Synchronized model.

A thread-unsafe object will throw the following exceptions:

* A <xref:PostSharp.Patterns.Threading.ThreadAccessException> whenever some code tries to access a field from a thread that does not own the correct lock, i.e. the call stack does not contain a public or internal method of this method (e.g. a private delegate call). 

* A <xref:PostSharp.Patterns.Threading.ConcurrentAccessException> when two public or internal methods execute at the same time on the same object (i.e. whenever the lock cannot be acquired without waiting). 

## See Also

**Reference**

<xref:PostSharp.Aspects.InstanceLevelAspect>
<br><xref:PostSharp.Patterns.Threading.ThreadUnsafeAttribute>
<br><xref:PostSharp.Patterns.Threading.ExplicitlySynchronizedAttribute>
<br><xref:PostSharp.Patterns.Threading.EntryPointAttribute>
<br>**Other Resources**

<xref:threading-waiving-verification>
<br><xref:ui-dispatching>
<br>