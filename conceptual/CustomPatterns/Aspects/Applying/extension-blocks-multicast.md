---
uid: extension-blocks-multicast
title: "C# 14 Extension Blocks and Multicasting"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "Explains how C# 14 extension blocks are handled by PostSharp's multicast engine in PostSharp 2026.0, including the AllowExtensionBlockMembers property."
---
# C# 14 Extension Blocks and Multicasting


## Overview

Starting with PostSharp 2026.0, the multicast engine provides support for C# 14 extension blocks. By default, aspects and multicast attributes are not applied to extension block members to prevent unexpected behavior. You can explicitly enable this using the <xref:PostSharp.Extensibility.MulticastAttribute.AllowExtensionBlockMembers> property.

## How Extension Blocks Are Implemented in IL

The C# compiler implements extension block members as static methods (including properties) and a set of special metadata types. These metadata types are intended for the C# compiler to match extension methods and properties with the receiver type. 

Since both the implementation methods and the metadata types may not be expected by existing aspects, the multicasting algorithm in PostSharp 2026.0 and later skips all of these members and adds explicit opt-in support for extension block implementation methods.

## Enabling Extension Block Support

To apply aspects to extension block members, one of the following actions needs to be taken:

- Add the aspect directly to the extension block member, or
- Set the <xref:PostSharp.Extensibility.MulticastAttribute.AllowExtensionBlockMembers> property to `true` when multicasting the aspect from an outer scope, such as the declaring type.
- Add <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute> to the aspect type and set <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExtensionBlockMembers> to `true`.

### Example: Applying aspects directly to extension members

When applied explicitly to extension members, aspects are added without the need to change any of the properties.

```csharp
[PSerializable]
class LoggingAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Entering: {args.Method.Name}");
    }
}

public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        [LoggingAspect]
        public TInstance ExtensionMethod() => instance;

        [LoggingAspect]
        public TInstance ExtensionProperty
        {
            get => instance;
            set { }
        }
    }
}
```

### Example: Multicasting with AllowExtensionBlockMembers set to true

After setting <xref:PostSharp.Extensibility.MulticastAttribute.AllowExtensionBlockMembers> to `true` and multicasting on the whole class, members within extension blocks will be eligible for the aspect.

```csharp
[PSerializable]
class LoggingAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Entering: {args.Method.Name}");
    }
}

[LoggingAspect(AllowExtensionBlockMembers = true)]
public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        public TInstance ExtensionMethod() => instance;

        public TInstance ExtensionProperty
        {
            get => instance;
            set { }
        }
    }
}
```

### Example: Changing default behavior for custom aspects

Setting the <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExtensionBlockMembers> property to `true` on the aspect class will make extension members automatically eligible for the aspect:

```csharp
[MulticastAttributeUsage(MulticastTargets.Method, AllowExtensionBlockMembers = true)]
[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Entering: {args.Method.Name}");
    }
}

[LoggingAspect]
public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        public TInstance ExtensionMethod() => instance;

        public TInstance ExtensionProperty
        {
            get => instance;
            set { }
        }
    }
}
```


## Backward Compatibility

Classic extension methods using the `this` modifier continue to work as before and are not affected by the `AllowExtensionBlockMembers` property. They are always eligible for aspect application according to existing multicast rules.


## See Also

<xref:multicast-conceptual>
<br><xref:attribute-multicasting>
<br><xref:whats-new-20260>
<br><xref:PostSharp.Extensibility.MulticastAttribute.AllowExtensionBlockMembers>
<br><xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExtensionBlockMembers>

**Other Resources**
<br>[C# 14 Extension Members - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extension)
<br>[Exploring extension members - .NET Blog](https://devblogs.microsoft.com/dotnet/csharp-exploring-extension-members/)
