---
uid: licensing-shared-source-code
title: "Sharing Source Code With Unlicensed Teams"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "PostSharp requires a license only for creating or modifying code, not for building it. To share source code with unlicensed teams, enable source control checking in the PostSharp Configuration File. "
---
# Sharing Source Code With Unlicensed Teams

You only need a license if you *create or modify* code using PostSharp. If you only *build* code that is using PostSharp, you don't need to purchase a license. 

PostSharp can determine whether you modify the code or just build it by looking at your source control repository. If your working copy has modifications against your base commit in your source control repository, PostSharp will consider that you are creating or modifying the code yourself, and will require a valid license.

By default, checking the modifications in the source control is disabled for performance reasons. It means that by default PostSharp always requires a valid license during the build. You can enable source control checking by editing the *PostSharp Configuration File* for your project or solution (see <xref:configuration-system>). 


### To enable source code sharing with unlicensed teams:

1. Open the file *postsharp.config* that is located in the root directory of your solution or project. If the file doesn't exist then create a new *postsharp.config* file in that location with the following content: 

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <Project xmlns="http://schemas.postsharp.org/1.0/configuration">
    </Project>
    ```


2. Add a Property element under the Project element, set the Name attribute to `VcsCheckEnabled` and the Value attribute to `True`. 

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <Project xmlns="http://schemas.postsharp.org/1.0/configuration">
        <Property Name="VcsCheckEnabled" Value="True" />
    </Project>
    ```


> [!NOTE]
> The following source control systems are supported: **Git** and **TFS**. If you are not using any of these systems, PostSharp will always require a license at build time. 

> [!WARNING]
> When building unmodified source code without a commercial license, PostSharp Logging Developer Edition and limitations to PostSharp Logging features will apply to your build. See <xref:logging-license> for details. 





