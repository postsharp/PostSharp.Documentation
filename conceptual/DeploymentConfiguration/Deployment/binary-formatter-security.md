---
uid: binary-formatter-security
title: "BinaryFormatter security"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The .NET Core 3.1 BinaryFormatter, used for binary serialization, is considered insecure. PostSharp aspects are serialized at compile-time, but the vulnerability doesn't apply to PostSharp's use of serialization. The recommended action is to use PortableFormatter for serializing aspects."
---
# BinaryFormatter security

In .NET Core 3.1 `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter`, which is used for binary serialization of CLR object, began to be considered insecure and dangerous. In .NET 5.0, `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter` started to throw an exception upon its use in ASP.NET Core applications. In .NET 8.0, more serialization-related APIs started to be obsolete and by default, BinaryFormatter
is disabled (throwing exceptions) in all .NET 8.0 projects with an exception of WinForms and WPF projects.

The attack vector of this vulnerability is deserialization of data that could be manipulated by the attacker, which can result in execution of arbitrary command under credentials of the process that executed the deserialization.


## Impact of vulnerability on PostSharp

PostSharp aspects are serialized at compile-time to preserve aspect parameters and analysis results and stored in a managed resource within the assembly. Serialized aspects are deserialized either at compile time to facilitate aspect inheritance (e.g. the aspect is applied to a derived class), or at run time when aspect is about to be used.

The above described **vulnerability does not apply to the use of serialization by PostSharp itself**, because in order to alter the serialized data, the attacker would have to alter the assembly containing the data. Being able to alter an assembly containing the serialized aspect data is itself a more general security vulnerability. 

PostSharp allows multiple methods of serializing aspects. The `T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter` is used if the type has the `[Serializable]` attribute in C#. After PostSharp 4.0, the preferred method of serialization is using `T:PostSharp.Serialization.PSerializableAttribute`, which results in use of `T:PostSharp.Serialization.PortableFormatter`, which is our own efficient and portable serialization format for aspect serialization. 

Using `[Serializable]` on aspect classes will result in a build-time error LA206. 

In legacy applications that require usage of binary serialization, you can disable this error by setting `PostSharpBinaryFormatterAllowed` MSBuild property to `true`.

> [!CAUTION]
> Disabling LA206 and using `PostSharpBinaryFormatterAllowed` is not recommended.

> [!NOTE]
> In releases before PostSharp 2024.0, using `[Serializable]` may result in build-time warning LA205, which can be suppressed through `NoWarn` MSBuild property.

## Recommended actions

Since the usage of BinaryFormatter is discouraged by Microsoft and in most projects using it would cause a runtime exception, it is required to use PortableFormatter for serializing aspects instead.

All aspects and types that are used for aspect serialization should use `[PSerializable]` instead of `[Serializable]`.

## See Also

**Other Resources**

<xref:aspect-serialization>
<br>**Reference**

<xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter>
<br><xref:PostSharp.Serialization.PSerializableAttribute>
<br>[BinaryFormatter serialization methods are obsolete and prohibited in ASP.NET apps](https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/5.0/binaryformatter-serialization-obsolete)
<br>[BinaryFormatter security guide](https://docs.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide)
<br>
