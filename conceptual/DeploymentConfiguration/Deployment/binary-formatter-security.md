---
uid: binary-formatter-security
title: "BinaryFormatter security"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The .NET Core 3.1 BinaryFormatter, used for binary serialization, is considered insecure. PostSharp aspects are serialized at compile-time, but the vulnerability doesn't apply to PostSharp's use of serialization. The recommended action is to use PortableFormatter for serializing aspects."
---
# BinaryFormatter security

As of .NET Core 3.1 `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter`, which is used for binary serialization of CLR object, is considered insecure and dangerous. Starting with .NET 5.0, `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter` throws an exception upon its use in ASP.NET Core applications. 

The attack vector of this vulnerability is deserialization of data that could be manipulated by the attacker, which can result in execution of arbitrary command under credentials of the process that executed the deserialization.


## Impact of vulnerability on PostSharp

PostSharp aspects are serialized at compile-time to preserve aspect parameters and analysis results and stored in a managed resource within the assembly. Serialized aspects are deserialized either at compile time to facilitate aspect inheritance (e.g. the aspect is applied to a derived class), or at run time when aspect is about to be used.

The above described **vulnerability does not apply to the use of serialization by PostSharp itself**, because in order to alter the serialized the data, the attacker would have to alter the assembly containing the data. Being able to alter an assembly containing the serialized aspect data is itself more general security vulnerability. 

PostSharp allows multiple methods of serializing aspects. The `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter` is used if the type has the `[Serializable]` attribute in C#. After PostSharp 4.0, the preferred method of serialization is using `T:PostSharp.Serialization.PSerializableAttribute`, which results in use of `T:PostSharp.Serialization.PortableFormatter`, which is our own efficient and portable serialization format for aspect serialization. 


## Recommended actions

Since the usage of BinaryFormatter is not recommended by Microsoft and on some frameworks use of it would cause runtime exception, it is recommended to use PortableFormatter for serializing aspects instead.

All aspects and types that are used for aspect serialization should use `[PSerializable]` instead or `[Serializable]`. 

In projects targeting .NET Standard 2.0+, .NET Core 2.1+ and .NET 5.0+, PostSharp will produce a LA0205 warning if an aspect using `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter` aspect is used. 

## See Also

**Other Resources**

<xref:aspect-serialization>
<br>**Reference**

<xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter>
<br><xref:PostSharp.Serialization.PSerializableAttribute>
<br>[BinaryFormatter serialization methods are obsolete and prohibited in ASP.NET apps](https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/5.0/binaryformatter-serialization-obsolete)
<br>[BinaryFormatter security guide](https://docs.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide)
<br>
