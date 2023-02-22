---
uid: aspect-provider
title: "Adding Aspects Dynamically"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Aspects Dynamically

Additionally to providing advices, an aspect can provide other aspects dynamically using <xref:PostSharp.Aspects.IAspectProvider>. This allows aspect developers to address situations where it is not possible to add aspects declaratively (using custom attributes) to the source code; aspects can be provided on the basis of a complex analysis of the target assembly using <xref:System.Reflection>, or by reading an XML file, for instance. 

For details about <xref:PostSharp.Aspects.IAspectProvider>, see <xref:iaspectprovider>. 

## See Also

**Reference**

<xref:PostSharp.Aspects.IAspectProvider>
<br>**Other Resources**

<xref:iaspectprovider>
<br>