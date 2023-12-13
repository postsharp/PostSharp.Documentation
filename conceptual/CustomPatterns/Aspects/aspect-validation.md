---
uid: aspect-validation
title: "Validating Aspect Usage"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document discusses validating aspect usage in PostSharp. It explains how to use MulticastAttributeUsage, implement CompileTimeValidate, and use message sources for error handling. It also provides code examples."
---
# Validating Aspect Usage

Some aspects make sense only on a specific subset of targets. For instance, an aspect may require being applied to non-static methods only. Another aspect may not be compatible with methods that have ref or out parameters. If these constraints are not respected, these aspects will fail at run time. However, defects detected by the compiler are always cheaper to fix than ones detected later. So, as the developer of an aspect, you should ensure that the build will fail if your aspect is being used on an invalid target.


## Using [MulticastAttributeUsage]

The first level of protection is to configure multicasting properly with [<xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute>], as described in the article <xref:multicast>. However, this approach can only filter based on characteristics that are supported by the multicasting component. 


## Implementing CompileTimeValidate

The best way to validate aspect usage is to override the <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> method of your aspect class. 

In this example, we will show how an aspect `RequirePermissionAttribute` can require being applied only to methods of types that implement the `ISecurable` interface. 


### 

1. Inherit from one of the pre-built aspects. In this case, <xref:PostSharp.Aspects.OnMethodBoundaryAspect>. 

    ```csharp
    public class RequirePermissionAttribute: OnMethodBoundaryAspect
    ```


2. Override the <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> method. 

    ```csharp
    public override bool CompileTimeValidate(MethodBase target) 
    {
    ```


3. Perform a check to see if the target class implements the interface in question.

    ```csharp
    Type targetType = target.DeclaringType; 
    if (!typeof(ISecurable).IsAssignableFrom(targetType)) 
    { 
     
    }
    ```


4. If the target does not implement the interface you must signal the compilation process that this target should not have the aspect applied to it. There are two ways to do this. The first option is to throw an <xref:PostSharp.Extensibility.InvalidAnnotationException>. 

    ```csharp
    if (!typeof(ISecurable).IsAssignableFrom(targetType)) 
    { 
      throw new InvalidAnnotationException("The target type does not implement ISecurable."); 
    }
    ```


5. The second option is to emit an error message to the compilation process.

    ```csharp
    if (!typeof(ISecurable).IsAssignableFrom(targetType)) 
    { 
      Message.Write(SeverityType.Error, "Custom01", 
                    "The target type does not implement ISecurable.", target); 
     return false; 
    }
    ```


> [!NOTE]
> You may have noticed that <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> returns a boolean value. If you only return `false` from this method the compilation process will silently ignore it. You must either throw the <xref:PostSharp.Extensibility.InvalidAnnotationException> or emit an error message to not silently ignore the `false` return value. 

Making use of the <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> method is a great way to encode custom rules for applying aspects to target code. While it could be used to duplicate the functionality of the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes> or <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMemberAttributes>, its real power is to go beyond those filtering techniques. By using <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> you are able to filter aspect application in any manner that you can interrogate your codebase using reflection. 


## Using Message Sources

If you plan to raise many messages, you may prefer to define your own <xref:PostSharp.Extensibility.MessageSource>. A <xref:PostSharp.Extensibility.MessageSource> is backed by a managed resource mapping error codes to error messages. 

In order to create your own <xref:PostSharp.Extensibility.MessageSource>, you should: 

* Create an implementation of the <xref:PostSharp.Extensibility.IMessageDispenser>. Typically, implement the <xref:PostSharp.Extensibility.IMessageDispenser.GetMessage(System.String)> method using a large `switch` statement. To each message will correspond a string 

* Create a static instance of the <xref:PostSharp.Extensibility.MessageSource> class for your message source. 
    For instance, the following code defines a message source based on a message dispenser:
    ```csharp
    internal class ArchitectureMessageSource : MessageSource
    {
        public static readonly ArchitectureMessageSource Instance = new ArchitectureMessageSource();
    
        private ArchitectureMessageSource() : base( "PostSharp.Architecture", new Dispenser() )
        {
        }
    
        private class Dispenser : MessageDispenser
        {
            public Dispenser() : base( "CUS" )
            {
            }
    
            protected override string GetMessage( int number)
            {
                switch ( number )
                {
                    case 1:
                        return "Interface {0} cannot be implemented by {1} because of the [InternalImplement] constraint.";
    
                    case 2:
                        return "{0} {1} cannot be referenced from {2} {3} because of the [ComponentInternal] constraint.";
    
                    case 3:
                        return "Cannot use [ComponentInternal] on {0} {1} because the {0} is not internal.";
    
                    case 4:
                        return "Cannot use [Internal] on {0} {1} because the {0} is not public.";
                        
                    default:
                        return null;
                }
            }
        }
    }
    ```


* Then you can use a convenient set of methods on your `MessageSource` object: 
    ```csharp
    MyMessageSource.Instance.Write( classType, SeverityType.Error, "CUS001", new object[] { interfaceType, classType } );
    ```


> [!NOTE]
> You can also emit information and warning messages.

> [!TIP]
> Use <xref:PostSharp.Reflection.ReflectionSearch> to perform complex queries over `System.Reflection`. 


## Validating Attributes That Are Not Aspects

You can validate any attribute derived from <xref:System.Attribute> by implementing the interface <xref:PostSharp.Extensibility.IValidableAnnotation>. 

## See Also

**Reference**

<xref:PostSharp.Aspects.MethodLevelAspect.CompileTimeValidate(System.Reflection.MethodBase)>
<br><xref:PostSharp.Aspects.TypeLevelAspect.CompileTimeValidate(System.Type)>
<br><xref:PostSharp.Aspects.FieldLevelAspect.CompileTimeValidate(System.Reflection.FieldInfo)>
<br><xref:PostSharp.Aspects.LocationLevelAspect.CompileTimeValidate(PostSharp.Reflection.LocationInfo)>
<br><xref:PostSharp.Aspects.EventLevelAspect.CompileTimeValidate(System.Reflection.EventInfo)>
<br><xref:PostSharp.Aspects.AssemblyLevelAspect.CompileTimeValidate(System.Reflection.Assembly)>
<br><xref:PostSharp.Extensibility.IValidableAnnotation>
<br><xref:PostSharp.Extensibility.MessageSource>
<br><xref:PostSharp.Extensibility.Message>
<br><xref:PostSharp.Extensibility.Message.Write*>
<br>
