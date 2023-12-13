---
uid: adding-contracts
title: "Adding Contracts to Code"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Contracts to Code

This section describes how to add a contract to a field, property, or parameter.


## Introduction

Consider the following method which checks if a valid string has been passed in:

```csharp
public class CustomerModel
{
  public void SetFullName(string firstName, string lastName)
  {
      if(firstName == null)
        throw NullReferenceException();

      if(lastName == null)
        throw NullReferenceException();

      this.FullName = firstName + " " + lastName;
  }
}
```

In this example, checks have been added to ensure that both parameters contain a valid string. A better solution is to place the logic which performs this check into its own reusable class, especially such boilerplate logic is involved, and then reuse/invoke this class whenever the check needs to be performed.

PostSharp’s Contract attributes do just that by moving such checks out of code and into parameter attributes. For example, PostSharp’s <xref:PostSharp.Patterns.Contracts.RequiredAttribute> contract could be used to simplify the example as follows: 

```csharp
public class CustomerModel
{
    public void SetFullName([Required] string firstName, [Required] string lastName)
    {
        this.FullName = firstName + " " + lastName;
    }
}
```

In this example, the <xref:PostSharp.Patterns.Contracts.RequiredAttribute> attribute performs the check for null, thus eliminating the need to write the boiler plate code for the check in line with other code. 

A contract can also be applied to a property or field as shown in the following example:

```csharp
public class CustomerModel
{
    [Required]
    public FirstName { get; set; }
}
```

Using a contract in a property ensures that the value being passed into set is validated before the logic (if any) for set is executed.

Similarly, a contract can be used directly on a field which will validate the value being assigned to the field:

```csharp
public class CustomerModel
{
    [Required]
    private string mFirstName = "Not filled in yet";

    public void SetFirstName(string firstName)
    {
        mFirstName = firstName;
    }
}
```

In this example, `firstName` will be validated by the `Required` contract before being assigned to `mFirstName`. Placing a contract on a field provides the added benefit of validating the field regardless of where it’s set from. 

Note that PostSharp also includes a number of built-in contracts which range from checks for null values to testing for valid phone numbers. You can also develop your own contracts with custom logic for your own types as described below.

There are two ways to add contracts:


## Adding contracts


### To add a contract to an element of code:

1. Add the [PostSharp.Patterns.Common](https://www.nuget.org/packages/PostSharp.Patterns.Common/) package to your project. 


2. Import the <xref:PostSharp.Patterns.Contracts> namespace into your file: 

    ```csharp
    using PostSharp.Patterns.Contracts;
    ```


3. Add the contract custom attribute to the parameter, field, property, or return value. See the <xref:PostSharp.Patterns.Contracts> namespace for a list of available contract attributes. For example: 

    ```csharp
    [return: Required]
    public string SetFullName([Required] string firstName, [Required] string lastName)
    ```

    This example will make sure that both the parameters and the return value are not null.



## Contract inheritance

PostSharp ensures that any contracts which have been applied to an abstract, virtual, or interface method are inherited along with that method in derived classes, all without the need to re-specify the contract in the derived methods. This is shown in the following example:

```csharp
public interface ICustomerModel
{
  void SetFullName([Required] string firstName, [Required] string lastName);
}

public class CustomerModel : ICustomerModel
{
  public void SetFullName(string firstName, string lastName)
  {
     this.FullName = firstName + " " + lastName;
  }

}
```

Here `ICustomerModel.SetFullName` method specifies that the `firstName` and `lastName` parameters are required using the <xref:PostSharp.Patterns.Contracts.RequiredAttribute> attribute. Since the `CustomerModel.SetFullName` method implements this method, these attributes will also be applied to its parameters. 

> [!NOTE]
> If the derived class exists in a separate assembly, that assembly must be processed by PostSharp and must reference the `PostSharp` and `PostSharp.Patterns.Model` package 

