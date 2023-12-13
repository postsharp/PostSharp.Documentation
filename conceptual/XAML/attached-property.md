---
uid: attached-property
title: "Attached Property"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document explains how to create and use attached properties in XAML using PostSharp's AttachedPropertyAttribute aspect. It also discusses its relationship with DependencyPropertyAttribute aspect."
---
# Attached Property

In XAML, [Attached properties](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/attached-properties-overview) are a special kind of properties that are settable on a different object than the one that exposes it. An example of attached property is `DockPanel.Dock`. Attached properties are a special kind of dependency properties and thus require the same amount of boilerplate code to be written. 

The <xref:PostSharp.Patterns.Xaml.AttachedPropertyAttribute> aspect allows you to automate the implementation of attached properties in your classes and eliminates the boilerplate. 

The <xref:PostSharp.Patterns.Xaml.AttachedPropertyAttribute> aspect is a special case of the <xref:PostSharp.Patterns.Xaml.DependencyPropertyAttribute> aspect. Therefore you can refer to the <xref:dependency-property> article for additional documentation on topics such as validation, property change callbacks, and naming conventions. 


## Creating a simple attached property


### To add a new attached property to your class

1. Add a new static property to your class with a chosen name, a public getter and private setter. Declare the property type as `Attached<T>` where T is the desired type of your attached property (see <xref:PostSharp.Patterns.Xaml.Attached`1>). 


2. Mark your new property with the <xref:PostSharp.Patterns.Xaml.AttachedPropertyAttribute> attribute. 


3. Create a method which calls `SetValue` on the attached property. This method must be named SetX, where X is the name of the attached property and must look as follows: 

    ```csharp
    [public static void SetDock(DependencyObject host, string value) => Dock.SetValue(host, value);
    ```

    > [!NOTE]
    > Why can't PostSharp create the "Set" method boilerplate automatically? It's because PostSharp runs after compilation, but this code must already exist prior to XAML compilation.


```csharp
[AttachedProperty]
public static Attached<Dock> Dock { get; set; }
public static void SetDock(DependencyObject host, string value) => Dock.SetValue(host, value);
```


## Using the dependency property

After you have marked your property with the <xref:PostSharp.Patterns.Xaml.AttachedPropertyAttribute> attribute, you can directly start to use it in XAML. For example, you can set the value of the property on any object using the attached property syntax in XAML. 

```xml
<MyDockPanel>
  <CheckBox MyDockPanel.Dock="Top">Hello</CheckBox>
</MyDockPanel>
```

You can also read and write the property value on any object in C# by using the <xref:PostSharp.Patterns.Xaml.Attached`1.GetValue(System.Windows.DependencyObject)> and <xref:PostSharp.Patterns.Xaml.Attached`1.SetValue(System.Windows.DependencyObject,`0)> methods. 

```csharp
MyDockPanel.Dock.SetValue(this.HelloCheckBox, Dock.Top);
```

To get a <xref:System.Windows.DependencyProperty> instance that is backing your attached property, read the value of the <xref:PostSharp.Patterns.Xaml.Attached`1.RuntimeProperty> property. 

```csharp
MyDockPanel.Dock.RuntimeProperty.OverrideMetadata( typeof(MyPropertiesListView), new PropertyMetadata(Dock.Right) ));
```

## See Also

**Other Resources**

[Attached Properties Overview](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/attached-properties-overview)
<br><xref:dependency-property>
<br>**Reference**

<xref:PostSharp.Patterns.Xaml.AttachedPropertyAttribute>
<br><xref:PostSharp.Patterns.Xaml.DependencyPropertyAttribute>
<br><xref:System.Windows.DependencyProperty>
<br><xref:PostSharp.Patterns.Xaml.Attached`1>
<br><xref:PostSharp.Patterns.Xaml.Attached`1.GetValue(System.Windows.DependencyObject)>
<br><xref:PostSharp.Patterns.Xaml.Attached`1.SetValue(System.Windows.DependencyObject,`0)>
<br><xref:PostSharp.Patterns.Xaml.Attached`1.RuntimeProperty>
<br>
