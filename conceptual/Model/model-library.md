---
uid: model-library
title: ""
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
The Model Pattern Library provides the following features:

* **INotifyPropertyChanged.** The <xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute> aspect implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface and automatically raises the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event for the relevant properties whenever an object is changed. For details, see <xref:inotifypropertychanged>. 

* **Code Contracts**. The <xref:PostSharp.Patterns.Contracts> namespace contains custom attributes such as <xref:PostSharp.Patterns.Contracts.RequiredAttribute>, which can be applied to fields, properties or parameters, and validates their value at runtime. See <xref:contracts> for details. 

* **Aggregatable**. The <xref:PostSharp.Patterns.Model.AggregatableAttribute> aspect allows to automate the implementation of parent-child relationships, including automatic synchronization of the `Parent` property and implementation of the Visitor pattern for children. For more information see <xref:aggregatable>. 

* **Disposable**. The <xref:PostSharp.Patterns.Model.DisposableAttribute> aspect implements the <xref:System.IDisposable> interface and can automatically call the `System.IDisposable.Dispose()` method on child objects. More details can be found at <xref:disposable>. 

## See Also

**Reference**

<xref:PostSharp.Patterns.Model.NotifyPropertyChangedAttribute>
<br><xref:PostSharp.Patterns.Contracts>
<br>**Other Resources**

<xref:inotifypropertychanged>
<br><xref:contracts>
<br><xref:inotifypropertychanged-customization>
<br><xref:inotifypropertychanged-dependencies>
<br>