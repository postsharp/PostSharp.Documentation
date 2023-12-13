---
uid: attribute-multicasting
title: "Adding Aspects to Multiple Declarations Using Attributes"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document describes how to use PostSharp's custom attribute multicasting to apply aspects to multiple declarations in an application code, including applying to all class members, all types in a namespace, excluding some members, and filtering by class visibility or method modifiers."
---
# Adding Aspects to Multiple Declarations Using Attributes

Having written an aspect we have to apply it to the application code so that it will be used. There are a number of ways to do this so let's take a look at one of them: custom attribute multicasting. Other ways include XML Multicasting (see the section <xref:xml-multicasting>) and dynamic aspect providers (see more in the section <xref:iaspectprovider>). 


## Applying to all members of a class

When we are trying to apply a method-level aspect, we can place an attribute to each of the methods. As our codebase grows, this approach becomes tedious. We need to remember to add the attribute to all of the methods on the class. If you have hundreds of classes, you may have thousands of methods you need to manually add the aspect attribute to. It is an unsustainable proposition. Thankfully, there is a way to make this easier. Instead of applying your aspect to each method you can add that attribute to the class and PostSharp will ensure that the aspect is applied to all of the methods on that class.

```csharp
[OurLoggingAspect] 
public class CustomerServices
```

You can also add a location-level aspect to a class which applies it to all properties and all fields in that class. Note that this includes backing fields of auto-implemented properties.


## Applying an aspect to all types in a namespace

Even though we don't have to apply an aspect to all methods in all classes in our application, adding the aspect attribute to every class could still be an overwhelming task. If we want to apply our aspect in a broad stroke we can make use of PostSharp's <xref:PostSharp.Extensibility.MulticastAttribute>. 

The <xref:PostSharp.Extensibility.MulticastAttribute> is a special attribute that will apply other attributes throughout your codebase. Here's how we would use it. 


### 

1. Open the `AssemblyInfo.cs`, or create a new file `GlobalAspects.cs` if you prefer to keep things separate (the name of this file does not matter). 


2. Add an `[assembly:]` attribute that references the aspect you want to apply. 


3. Add the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypes> property to the aspect constructor and define the namespace that you would like the aspect applied to. 

    ```csharp
    [assembly: OurLoggingAspect(AttributeTargetTypes="OurCompany.OurApplication.Controllers.*")]
    ```


This one line of code is the equivalent of adding the aspect attribute to every class in the desired namespace.

> [!NOTE]
> When setting the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypes> you can use wildcards (`*`) to indicate that all sub-namespaces should have the aspect applied to them. It is also possible to indicate the targets of the aspect using `regex`. Add `"regex:"` as a prefix to the pattern you wish to use for matching. 


## Excluding an aspect from some members

Multicasting an attribute can apply the aspect with a very broad brush. It is possible to use <xref:PostSharp.Extensibility.MulticastAttribute.AttributeExclude> to restrict where the aspect is attached. 

```csharp
[assembly: OurLoggingAspect(AttributeTargetTypes="OurCompany.OurApplication.Controllers.*", AttributePriority = 1)] 
[assembly: OurLoggingAspect(AttributeTargetMembers="Dispose", AttributeExclude = true, AttributePriority = 2)]
```

In the example above, the first multicast line indicates that the `OurLoggingAspect` should be attached to all methods in the `Controllers` namespace. The second multicast line indicates that the `OurLoggingAspect` should not be applied to any method named `Dispose`. 

> [!NOTE]
> Notice the <xref:PostSharp.Extensibility.MulticastAttribute.AttributePriority> property that is set in both of the multicast lines. Since there is no guarantee that the compiler will apply the attributes in the order you have specified in the code, it is necessary to declare an order to ensure processing is completed as desired. 
In this case, the `OurLoggingAspect` will be applied to all methods in the `Controllers` namespace first. After that is completed, the second multicast of `OurLoggingAspect` is performed which then excludes the aspect from methods named `Dispose`. 

See <xref:multicast-override> for more details about excluding and overriding aspects. 


## Filtering by class visibility

Now that you've been able to apply our aspect to all classes in a namespace and its sub-namespaces, you may be faced with the need to restrict that broad stroke. For example, you may want to apply your aspect only to classes defined as being public.


### 

1. Add the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes> property to the <xref:PostSharp.Extensibility.MulticastAttribute> constructor. 


2. Set the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes> value to `Public`. 

    ```csharp
    [assembly: OurLoggingAspect(AttributeTargetTypes="OurCompany.OurApplication.Controllers.*",  
                                AttributeTargetTypeAttributes = MulticastAttributes.Public)]
    ```


By combining <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes> values you are able to create many combinations that are appropriate for your needs. 

> [!NOTE]
> When specifying attributes of target members or types, do not forget to provide all categories of flags, not only the category on which you want to put a restriction.


## Filtering by method modifiers

Filtering at the class level may not be granular enough for your needs. Aspects can be attached at the method level and you will want to control filtering on these aspects as well. Let's look at an example of how to apply aspects only to methods marked as virtual.


### 

1. Add the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMemberAttributes> property to the <xref:PostSharp.Extensibility.MulticastAttribute> 's constructor. 


2. Set the <xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetMemberAttributes> value to `Virtual`. 

    ```csharp
    [assembly: OurLoggingAspect(AttributeTargetTypes="OurCompany.OurApplication.Controllers.*", AttributeTargetMemberAttributes = MulticastAttributes.Virtual)]
    ```


Using this technique you can apply a method-level aspect, or stop it from being applied, based on the existence or non-existence of things like the static, abstract, and virtual keywords.


## Programmatic filtering

There are situations where you will want to filter in a way that isn't based on class or method declarations. You may want to apply an aspect only if a class inherits from a specific class or implements a certain interface. There needs to be a way for you to accomplish this.

The easiest way is to override the <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> method of your aspect class, where you can perform your custom filtering. This is the opt-out approach. Have the <xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)> method return `false` without throwing an exception, and the target candidate will be ignored. See the section <xref:aspect-validation> for details. 

The second approach is opt-in. See the section <xref:iaspectprovider> for details. 

## See Also

**Reference**

<xref:PostSharp.Extensibility.MulticastAttribute>
<br><xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypes>
<br><xref:PostSharp.Extensibility.MulticastAttribute.AttributeExclude>
<br><xref:PostSharp.Extensibility.MulticastAttribute.AttributePriority>
<br><xref:PostSharp.Extensibility.MulticastAttribute.AttributeTargetTypeAttributes>
<br><xref:PostSharp.Aspects.Aspect.CompileTimeValidate(System.Object)>
<br><xref:PostSharp.Extensibility.MulticastAttributeUsageAttribute.PersistMetaData>
<br>**Other Resources**

<xref:aspect-validation>
<br><xref:iaspectprovider>
<br><xref:xml-multicasting>
<br>
