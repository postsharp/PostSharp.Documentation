---
uid: extension-blocks-multicast
title: "C# 14 Extension Blocks and Multicasting"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "Explains how C# 14 extension blocks are handled by PostSharp's multicast engine in PostSharp 2026.0, including the AllowExtensionBlockMembers property."
---
# C# 14 Extension Blocks and Multicasting


## Overview

Starting with PostSharp 2026.0, the multicast engine provides support for C# 14 extension blocks. By default, aspects and multicast attributes are not applied to extension block members to prevent unexpected behavior. You can explicitly enable this using the `AllowExtensionBlockMembers` property.


## What Are Extension Blocks?

C# 14 introduces a new syntax for declaring extension members using `extension` blocks within static classes. Extension blocks allow you to define extension methods and properties, including static extension members that appear to extend the type itself rather than instances.

Example:

```csharp
public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        // Instance extension property
        public TInstance ExtensionProperty
        {
            get => instance;
            set { }
        }

        // Static extension property
        public static int StaticExtensionProperty
        {
            get => 42;
            set { }
        }

        // Instance extension method
        public TInstance ExtensionMethod() => instance;

        // Static extension method
        public static void StaticExtensionMethod() { }
    }
}
```


## How Extension Blocks Are Implemented in IL

In C# 14, extension blocks are implemented by the compiler as static methods and a set of special metadata types. These metadata types are intended for the C# compiler to match extension methods and properties with the receiver type. Since both the implementation methods and the metadata types may not be expected by existing aspects, PostSharp 2026.0 introduces explicit opt-in support into the multicast engine.


## Applying Aspects to Extension Block Members

By default, PostSharp does not apply aspects or multicast attributes to members declared within extension blocks. This is a safety measure to avoid unintended behavior, as extension block members have unique characteristics that differ from regular methods and properties.

Extension block metadata are always skipped by multicasting, but may still be targeted by <xref:PostSharp.Aspects.Advices.IAdviceProvider> and <xref:PostSharp.Aspects.IAspectProvider> by mistake. Doing this will result in an error.

### Enabling Extension Block Support

To apply aspects to extension block members, set the `AllowExtensionBlockMembers` property to `true`. This property is available on:

- <xref:PostSharp.Extensibility.MulticastAttribute.AllowExtensionBlockMembers> - Set this property when applying the aspect to control whether it targets extension block members.
- <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExtensionBlockMembers> - Set this property on the aspect class definition to change the default behavior.


### Example: Applying Aspects to Extension Methods

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
        [LoggingAspect(AllowExtensionBlockMembers = true)]
        public TInstance ExtensionMethod() => instance;

        [LoggingAspect(AllowExtensionBlockMembers = true)]
        public static void StaticExtensionMethod() { }
    }
}
```


### Example: Applying Aspects to Extension Property Accessors

Extension properties can also have aspects applied to their accessors:

```csharp
public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        public TInstance ExtensionProperty
        {
            [LoggingAspect(AllowExtensionBlockMembers = true)]
            get => instance;

            [LoggingAspect(AllowExtensionBlockMembers = true)]
            set { }
        }
    }
}
```


### Example: Changing Default Behavior for Custom Aspects

When developing a custom aspect, you can allow extension block members by default by setting the <xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.AllowExtensionBlockMembers> property on the aspect class:

```csharp
[MulticastAttributeUsage(MulticastTargets.Method, AllowExtensionBlockMembers = true)]
[PSerializable]
public class ExtensionFriendlyAspect : OnMethodBoundaryAspect
{
    // Aspect implementation
}
```


## Multicast Filtering Behavior

When applying aspects using multicast to a type containing extension blocks:

- **Default behavior**: Extension block members are excluded from multicasting
- **With `AllowExtensionBlockMembers = true`**: Extension block members are included in multicasting

Example of multicasting with extension blocks:

```csharp
[MulticastAttributeUsage(MulticastTargets.All, PersistMetaData = true, AllowExtensionBlockMembers = true)]
class AttributeWithExtensionMembers : MulticastAttribute
{
}

[AttributeWithExtensionMembers]
public static class TestClassExtensions
{
    extension<TInstance>(TInstance instance)
    {
        // This method will receive the attribute because AllowExtensionBlockMembers = true
        public TInstance ExtensionMethod() => instance;
    }

    // Classic extension methods are always included
    public static void ClassicExtensionMethod<TInstance>(this TInstance instance)
    {
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
