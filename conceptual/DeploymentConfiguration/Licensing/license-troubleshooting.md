---
uid: license-troubleshooting
title: "License Troubleshooting"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# License Troubleshooting

We've designed the PostSharp Licensing component so that it stands in your way as little as possible. However, there are legitimate cases in which we had to make the choice to prevent the build of your project. This article mentions these cases and gives solutions to such situations.

In case the solution given by this article doesn't work, or your case isn't listed, please contact our [support](https://www.postsharp.net/support). 

This article only talks about the technical issues you may encounter. You can find more answers to your questions about PostSharp licensing in our [licensing FAQ](https://www.postsharp.net/pricing/faq). 


## Error PS0242: License error. No valid license key has been installed. To register a license key, see https://doc.postsharp.net/deploying-keys.

In case you do have a license key installed as mentioned in the <xref:deploying-keys> and you're still getting this error, it most probably means that the version of PostSharp you are using has been released after the end of your subscription period. In this case, you either need to [renew your subscription](https://www.postsharp.net/pricing/upgrades-and-renewals) or to use an older version of PostSharp, that has been released before the end of your subscription period. 


## Error PS0243: License error. The project uses non-licensed premium features. It is not allowed to enhance types with a total of more than X
				lines of code in the project by features not covered by the installed licenses, but Y lines of code were enhanced.

There are two cases in which you might get such error:

* You are using PostSharp Essentials license:
    In this case, you've hit the limitation of the license. To diagnose this, follow the information given at [log](xref:express-limitations#diagnosing-licensing-issues). 

* You are using other license than PostSharp Essentials or PostSharp Ultimate license:
    If you don't have the unlimited PostSharp Ultimate license, any features not covered by your license are licensed as if you were using the PostSharp Essentials license.
    A quick way to figure out which features these are, is to look through all the namespace usings of namespaces `PostSharp.*`. 
    Another way is to follow the same information as for PostSharp Essentials as mentioned above.


## A message warning that a license or subscription has expired pops up after installing a renewed license key.

Uninstall the expired license key. See <xref:deploying-keys> to figure out where the expired license key can be installed. 


## A license key fails to uninstall.

There are various reasons for such behavior. A quick way to resolve this is to remove the `LicenseCache` and `LicenseKeys` registry keys from the following parent keys from Windows registry. 

* `Computer\HKEY_CURRENT_USER\SOFTWARE\SharpCrafters`
* `Computer\HKEY_LOCAL_MACHINE\SOFTWARE\SharpCrafters`
* `Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\SharpCrafters`

## A redistribution license doesn't allow to build a project, although it only contains code that belongs the licensed namespace.

Besides the namespace, the assembly name must also match the licensed namespace name. Make sure that the compiled assembly name starts with the licensed namespace.

For example, if your licensed namespace is `MyOssLib`, you assembly name must be also `MyOssLib`, or `MyOssLib.` followed by an arbitrary string. 


## PostSharp license server is closing connections unexpectedly.

Make sure that the PostSharp License Server runs on .NET Framework 4.7.2 or newer.

