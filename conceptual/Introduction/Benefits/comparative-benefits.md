---
uid: comparative-benefits
title: "Benefits of PostSharp vs Alternatives"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Benefits of PostSharp vs Alternatives

PostSharp is the #1 pattern-aware extension to C#/VB. It adds a concept of pattern to the languages, resulting in a dramatic reduction of boilerplate code, lower development and maintenance costs and fewer errors.


## Get more productive in minutes with ready-made pattern implementations

* **INotifyPropertyChanged Pattern**. Automates the implementation of INotifyPropertyChanged and automatically raises notifications for you. It also analyzes chains of dependencies between properties, methods and fields in your source code, and understands that property getters can access several fields and call different methods, or even depend on properties of other objects. PostSharp eliminates all the repetition and lets you go from three lines of code per property to one attribute per base class... so you will never forget to raise a property change notification again. 

* **Undo/Redo Pattern**. Makes the implementation of the end-users most-wanted features easy and affordable by recording changes at model level. Provides built-in user controls or allows you to create your own. You can deliver the familiar Undo/Redo experience to your users without getting stuck writing large amounts of code. 

* **Code Contracts**. Provide validation for valid URLs, email addresses, positive numbers or not-null values and many more, right out of the box. Allows you to use contract attributes without limitations at any location in your codebase and validate methods, fields, properties and parameters. This enables you to protect your code from invalid inputs with custom attributes. 

* **Logging Pattern**. Adds comprehensive logging in a few clicks – without impact on your source code – and lets you remove it just as quickly. Provides parameter and return values providing added information for maintenance and support work. Supports most popular backends, including log4net, NLog, Enterprise Library, System Console, System Diagnostics. You can trace everything you need in minutes without cluttering your code. 


## Automate more complex patterns and remove more boilerplate

* **PostSharp Aspect Framework**. PostSharp is hands down the most robust and exhaustive implementation of aspect-oriented programming for .NET and was evolved into the world's best pattern compiler. It is the most powerful toolset available to implement automation for your own patterns. 

* **Largest choice of possible transformations**. Includes decoration of methods, iterators and async state machines, interception of methods, events or properties, introduction of interfaces, methods, events, properties, custom attributes or resources, and more. 

* **Composition of several transformations** to easily automate complex patterns. 

* **Dynamic aspect/advice providers**. Addresses situations where it is not possible to add aspects declaratively (using custom attributes) to the source code with dynamic aspect/advice providers. 

* **Aspect inheritance**. Apply an aspect to a base class, specify that you want it to be inherited and all derived classes will automatically have the aspect applied to them. Relieves you from implementing the aspects manually and ensures that all derived classes using this aspect's logic is correct. 

* **Architecture framework**. Validates handwritten source code against your own custom pattern guidelines. It then express the rules in C# using the familiar System.Reflection API, extended with features commonly found in decompilers, such as “find usage”, and more. 


## Build thread-safe apps--without a PhD

Starting new threads and tasks in .NET languages is simple, but ensuring that objects are thread-safe is not with mainstream programming languages. That's why PostSharp extends C# and VB with thread-safety features.

* **7 different threading models**. Threading models are design patterns that guarantee your code executes safely even when used from multiple threads. Threading models raise the level of abstraction at which multithreading is addressed. Unlike working directly with locks and other low-level threading primitives, threading models decrease the number of lines of code, the number of defects and reduce development and maintenance costs – without having to have expertise in multithreading. Includes: 
* **Immutable Threading Model**. Allows you to make select objects in your codebase immutable so that they can be safely accessed by several threads concurrently, without the need for locking or other synchronization. 

* **Freezable Threading Model**. This is the milder brother of the Immutable pattern. It is suitable when you need to prevent changes to an instance of an object most of, but not all of the time. Lets you define the point in time where immutability begins. 

* **Synchronized Threading Model**. Makes sure the objects are accessed by a single thread at a time. Other threads will wait until the object is available so you'll avoid data races. 

* **Reader-Writer Synchronized Threading Model**. This pattern relies on the fact that most objects are much more often read than modified. Compared to traditional locking, it maximizes read throughput and minimizes the odds of deadlocks. 

* **Actor Threading Model**. Actors are classes that essentially run within a single thread. Other code communicates with actors using asynchronous calls. Celebrated by Erlang, Scala and F# developers, this pattern is now available to .NET thanks to PostSharp and C# 5.0. 

* **Thread Affine Threading Model**. Limits object instance access to the thread that created the instance. 

* **Thread Unsafe Threading Model**. Perfect pattern to make sure that objects will never be accessed concurrently by several threads. Get an exception instead of a random data corruption. 


* **Model validation**. Catches most defects during the build or during single-threaded test coverage. 

* **Thread dispatching patterns**. Causes the execution of a method to be dispatched to the UI thread or to a background thread. Much easier than using nested anonymous methods. 


## Maintain your existing codebase in C# or Visual Basic

Despite the hype around functional programming languages, C#/VB and .NET remain an excellent platform for enterprise development. PostSharp respects your technology assets and will work incrementally with your existing code base – there is NO need for a full rewrite or redesign.

* **Design neutrality**. Unlike alternatives, PostSharp takes minimal assumptions on your code. It does not force you to adopt any specific architecture or threading model. You can add aspects to anything, not just interface/virtual methods. Plus, it is fully orthogonal from dependency injection. You don't have to dissect your application into components and interfaces in order to use PostSharp. 

* **Plain C# and VB**. PostSharp provides advanced features present in F#, Scala, Nemerle, Python, Ruby or JavaScript, but your code is still 100% C# and VB, and it is still compiled by the proved Microsoft compilers. 

* **Cross-platform**. PostSharp supports the .NET Framework, Windows Phone, WinRT, Xamarin and Portable Class Libraries. 

* **Standard skill set**. No complex API. Reuse what you already know from C# and System.Reflection. 


## Benefit from much better run-time performance

Start-up latency, execution speed and memory consumption matter. Whether you're building a mobile app or a backend server, PostSharp delivers exceptional run-time performance.

* **Build-time code generation**. Unlike proxy-based solutions, PostSharp modifies your code at build time. It also allows for much more powerful enhancements that produces dramatically faster applications. 

* **No reflection**. PostSharp does not rely on reflection at run-time. The only code that is executed is what you can see with a decompiler. 

* **Build-time initialization**. Many patterns make decisions based on the shape of the code which they are applied. With PostSharp, you can analyze the target code at build-time and store the decisions into serializable fields. At runtime, the aspects will be deserialized and you won't need to analyze the code at run-time using reflection. 

