---
uid: global-service-container
title: "Using a Global Composition Container"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Using a Global Composition Container

Although the aspect cannot be instantiated by the dependency injection container, it is possible to initialize the aspect from an *ambient container* at run time. An ambient container is one that is exposed as a static member and that is global to the whole application. 

Dependency injection containers typically offer methods to initialize objects that have been instantiated externally. For instance, the Managed Extensibility Framework offers the <xref:System.ComponentModel.Composition.Hosting.CompositionContainer.SatisfyImportsOnce(System.ComponentModel.Composition.Primitives.ComposablePart)> method. 

The dependency injection method can be invoked from the <xref:PostSharp.Aspects.MethodLevelAspect.RuntimeInitialize(System.Reflection.MethodBase)> method. 

> [!NOTE]
> User code has no control over the time when and the thread on which an aspect is initialized. Therefore, using <xref:System.ThreadStaticAttribute> to make the container local to the current thread is not a reliable approach. 

> [!IMPORTANT]
> The service container must be initialized before the execution of any class that is enhanced by the aspect. It means that it is not possible to use the aspect on test classes themselves. To relax this constraint, it is possible to initialize the dependency lazily, when the first advice is hit.


## Example: testable logging aspect with a global MEF service container

The following code snippet shows a logging aspect and how it could be used in production code:

```csharp
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;

namespace DependencyResolution.GlobalServiceContainer
{
    public interface ILogger
    {
        void Log(string message);
    }

    public static class AspectServiceInjector
    {
        private static CompositionContainer container;

        public static void Initialize(ComposablePartCatalog catalog)
        {
            container = new CompositionContainer(catalog);
        }

        public static void BuildObject(object o)
        {
            if (container == null)
                throw new InvalidOperationException();

            container.SatisfyImportsOnce(o);
        }
    }

    [PSerializable]
    public class LogAspect : OnMethodBoundaryAspect
    {
        [Import] private ILogger logger;


        public override void RuntimeInitialize(MethodBase method)
        {
            AspectServiceInjector.BuildObject(this);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            logger.Log("OnEntry");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            AspectServiceInjector.Initialize(new TypeCatalog(typeof (ConsoleLogger)));

            // The static constructor of LogAspect is called before the static constructor of the type
            // containing target methods. This is why we cannot use the aspect in the Program class.
            Foo.LoggedMethod();
        }
    }

    internal class Foo
    {
        [LogAspect]
        public static void LoggedMethod()
        {
            Console.WriteLine("Hello, world.");
        }
    }

    [Export(typeof (ILogger))]
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
```

The following code snippet shows how the logging aspect can be tested:

```csharp
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyResolution.GlobalServiceContainer.Test
{
    [TestClass]
    public class TestLogAspect
    {
        static TestLogAspect()
        {
            AspectServiceInjector.Initialize(new TypeCatalog(typeof (TestLogger)));
        }

        [TestMethod]
        public void TestMethod()
        {
            TestLogger.Clear();
            new TargetClass().TargetMethod();
            Assert.AreEqual("OnEntry" + Environment.NewLine, TestLogger.GetLog());
        }

        private class TargetClass
        {
            [LogAspect]
            public void TargetMethod()
            {
            }
        }
    }

    [Export(typeof (ILogger))]
    internal class TestLogger : ILogger
    {
        public static readonly StringBuilder stringBuilder = new StringBuilder();

        public void Log(string message)
        {
            stringBuilder.AppendLine(message);
        }

        public static string GetLog()
        {
            return stringBuilder.ToString();
        }

        public static void Clear()
        {
            stringBuilder.Clear();
        }
    }
}
```

