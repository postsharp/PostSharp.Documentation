---
uid: custom-contracts
title: "Creating Custom Contracts"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Creating Custom Contracts

Given the benefits that contracts provide over manually checking values and throwing exceptions in code, you will likely want to implement your own contracts to perform your own custom checks and handle your own custom types.

The following steps show how to implement a contract which throws an exception if a numeric parameter is zero:


### To implement a contract throwing an exception if a numeric parameter is zero:

1. Use the following namespaces: <xref:PostSharp.Aspects> and <xref:PostSharp.Reflection>. 


2. Derive a class from <xref:PostSharp.Patterns.Contracts.LocationContractAttribute>: 

    ```csharp
    public class NonZeroAttribute : LocationContractAttribute
    {
        public NonZeroAttribute()
            : base()
        {
        }
    }
    ```


3. Implement the <xref:PostSharp.Aspects.ILocationValidationAspect> interface in the new contract class which exposes the <xref:PostSharp.Aspects.ILocationValidationAspect`1.ValidateValue(`0,System.String,PostSharp.Reflection.LocationKind,PostSharp.Aspects.LocationValidationContext)> method. Note that this interface must be implemented for each type that is to be handled by the contract. In this example, the contract will handle both `int` and `uint`, so the interface is implemented for both integer types. If additional integer types were to be handled by this class (e.g. `long`), then additional implementations of <xref:PostSharp.Aspects.ILocationValidationAspect> would have to be added: 

    ```csharp
    public class NonZeroAttribute : LocationContractAttribute, ILocationValidationAspect<int>, ILocationValidationAspect<uint>
    {
        public NonZeroAttribute()
            : base()
        {
        }
    
        public Exception ValidateValue(int value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value == 0)
                return new ArgumentOutOfRangeException($"The value of {locationName} cannot be 0.");
            else
                return null;
        }
        
        public Exception ValidateValue(uint value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value == 0)
                return new ArgumentOutOfRangeException($"The value of {locationName} cannot be 0.");
            else
                return null;
        }
    }
    ```

    The <xref:PostSharp.Aspects.ILocationValidationAspect`1.ValidateValue(`0,System.String,PostSharp.Reflection.LocationKind,PostSharp.Aspects.LocationValidationContext)> method takes in the value to test, the name of the parameter, property or field, and the usage (i.e. whether it’s a parameter, property, or field). The method must throw an exception if a check fails, or null or if no exception is to be raised. 


With the contract now created it can be used. For example, the following methods, which calculate the modulus between two numbers, can use the contract defined above to ensure that neither of their input parameters are zero:

```csharp
bool Mod([NonZero] int number, [NonZero] int dividend)
{
  return ((number % dividend) == 0);
}

bool Mod([NonZero] uint number, [NonZero] uint dividend)
{
  return ((number % dividend) == 0);
}
```

