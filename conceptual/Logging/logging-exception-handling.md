---
uid: logging-exception-handling
title: "Handling Logging Exceptions"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Handling Logging Exceptions

The last thing you want is to have your application to fail in production because of a failure of the logging feature. In order to prevent this from happening, PostSharp catches all exceptions that are thrown by the logging code and allows you to override the behavior.


## Default exception handling policy

By default, when an exception occurs in the logging feature, the following happens:

* A message is logged to the <xref:PostSharp.Patterns.Diagnostics.LoggingRoles.Meta> role. By default, the <xref:PostSharp.Patterns.Diagnostics.LoggingRoles.Meta> role uses the default <xref:PostSharp.Patterns.Diagnostics.LoggingBackend>, so it is likely that the logging of the logging failure will fail. It is recommended to configure the <xref:PostSharp.Patterns.Diagnostics.LoggingRoles.Meta> logging role separately by using the following code snippet: 
    ```csharp
    LoggingServices.Roles[LoggingRoles.Meta].Backend = new MyBackend();
    ```


* The whole <xref:PostSharp.Patterns.Diagnostics.LoggingBackend> responsible for the failure is disabled, unless the reason of the failure is <xref:PostSharp.Patterns.Diagnostics.LoggingBackend> is a defect in user code (typically an invalid use of the <xref:PostSharp.Patterns.Diagnostics.Logger> API) and not in the logging component itself. This pattern is called a *circuit breaker*. You can enable the <xref:PostSharp.Patterns.Diagnostics.LoggingBackend> again by setting the <xref:PostSharp.Patterns.Diagnostics.LoggingBackend.IsEnabled> property to `true`. 

> [!WARNING]
> When the logging exception happens during the logging of a user-code exception, the details of the logging exception are not available. The reason for this behavior is that user-code exceptions are logged from exception filters in order to preserve the call stack, and exception thrown in exception filters cannot be caught.


## Customizing the exception handling logic


### To override the default exception handling behavior:

1. Create a class that implements the <xref:PostSharp.Patterns.Diagnostics.ILoggingExceptionHandler> interface. 


2. Assign the <xref:PostSharp.Patterns.Diagnostics.LoggingServices.ExceptionHandler> property to an instance of your class. 


The following code snippet implements an exception handler that throws an exception upon any failure. The `Initialize` method registers the exception handler. 

```csharp
class ThrowLoggingExceptionHandler : ILoggingExceptionHandler
{
    public void OnInternalException( LoggingExceptionInfo exceptionInfo )
    {
        throw new Exception("Internal logging exception.", exceptionInfo.Exception);
    }

    public void OnInvalidUserCode( ref CallerInfo callerInfo, LoggingTypeSource source, string message, params object[] args )
    {
        throw new InvalidOperationException( string.Format( message, args ) );
    }
    
    public static void Initialize()
    {
        LoggingServices.ExceptionHandler = new ThrowLoggingExceptionHandler();
    }
}
```

## See Also

**Reference**

<xref:PostSharp.Patterns.Diagnostics.ILoggingExceptionHandler>
<br><xref:PostSharp.Patterns.Diagnostics.LoggingServices.ExceptionHandler>
<br><xref:PostSharp.Patterns.Diagnostics.LoggingBackend.IsEnabled>
<br>