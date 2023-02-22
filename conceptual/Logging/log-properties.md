---
uid: log-properties
title: "Adding Properties to Messages and Activities"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Adding Properties to Messages and Activities

When you write a message, open an activity, or close an activity, you can specify additional properties. Properties are name-value pairs that are passed to the logging backend. Unlike message parameters, properties are not rendered into the message text by default. When defined on activities, properties can be inherited by children contexts and activities.


## Defining properties thanks to an anonymous type

Every method that allows you to write a message, open an activity, or close an activity accepts an optional parameter named `options`. The type of this parameter has a constructor that takes a parameter of type `object` called `data`. You can set properties to the message or activity by passing an anonymous object to this constructor. 

For instance, the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.Write``1(``0@,PostSharp.Patterns.Diagnostics.WriteMessageOptions@)> method accepts an `options` parameter of type <xref:PostSharp.Patterns.Diagnostics.WriteMessageOptions>, and this type has a constructors that accepts an `object`. 

```csharp
logSource.Default.Write( Formatted( "Hello, World." ), 
                                     new  WriteMessageOptions ( new { User = User.Identity.Name,  Day = DateTime.Today.DayOfWeek } ) );
```

The code above adds two properties to the message: `User` and `Day`. 

You can pass any .NET type to the `data` parameter of the options constructor. By default, all public instance properties will be exported as logging properties unless they throw an exception. 


## Defining inherited properties

By default, all properties defined on an activity apply to the opening and closing message of that activity only. However, it is sometimes useful to include a property in all messages under this activity, under those under child activites. This can be done using inherited properties.

You can define an inherited property by annotating it with the <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute> custom attribute and setting its <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute.IsInherited> property to `true`. Of course, you cannot do that with an anonymous type, so you will have to define your own. 

You can also use other properties of <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute>: <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute.IsIgnored>, <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute.IsRendered> and <xref:PostSharp.Patterns.Diagnostics.Custom.LoggingPropertyOptionsAttribute.IsBaggage> 

For instance, the following class exports a single property to PostSharp Logging, and this property will be inherited by all children messages and activities:

```csharp
private class PropertiesWithAttributes
{
    [LoggingPropertyOptions( IsIgnored = true )]
    public string Ignored { get; set; }

    [LoggingPropertyOptions( IsInherited = true )]
    public string Inherited { get; set; }
}
```


## Advanced scenarios with logging properties

When you define properties using a normal .NET object, the object you're passing actually gets wrapped into a <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventData> This type has two properties: <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventData.Data> and <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventData.Metadata> The second property, of type <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventMetadata>, is responsible for mapping the value of the <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventData.Data> property to logging properties. You can customize this mapping by providing your own implementation of the <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventMetadata> class. 

This design, which separates data from metadata, allows to pass a several properties without even allocating a single object on the heap. For instance, you can pass an existing `HttpContext` to PostSharp Logging and use a custom <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventMetadata> to decide how this `HttpContext` should be presented to logging back-ends. 

The following examples shows how to expose the HTTP context to properties (this is an abbreviated version of what is implemented by <xref:PostSharp.Patterns.Diagnostics.Adapters.AspNetCore.AspNetCoreLogging>): 

```csharp
internal abstract class AspNetCoreMetadata : LogEventMetadata<AspNetCoreRequestExpressionModel>
    {
        private static readonly char[] separators = { ',' };


        public AspNetCoreMetadata() : base( "AspNetCoreRequest" )
        {
        }

        protected void Visit<TVisitorState>( string name, object value, ILoggingPropertyVisitor<TVisitorState> visitor, 
		                                     ref TVisitorState visitorState2, bool isBaggage = false )
        {
            if ( value != null && !(value is string s && string.IsNullOrEmpty( s )) )
            {
                visitor.Visit( name, value, LoggingPropertyOptions.Default.WithIsBaggage( isBaggage ), ref visitorState2 );
            }
        }

        protected void VisitProperties<TVisitorState>( HttpContext httpContext, ILoggingPropertyVisitor<TVisitorState> visitor, 
		                                               ref TVisitorState visitorState )
        {
            this.Visit( "User", httpContext.User?.Identity?.Name, visitor, ref visitorState );

            this.Visit( "RemoteIpAddress", httpContext.Connection.RemoteIpAddress, visitor, ref visitorState );
            this.Visit( "ConnectionId", httpContext.Connection.Id, visitor, ref visitorState );

            HttpRequest request = httpContext.Request;
            this.Visit( "Method", request.Method, visitor, ref visitorState );
            this.Visit( "Path", request.Path, visitor, ref visitorState );
            this.Visit( "Protocol", request.Protocol, visitor, ref visitorState );
            if ( request.QueryString.HasValue )
            {
                this.Visit( "QueryString", request.QueryString.Value, visitor, ref visitorState );
            }
			
            this.Visit( "Scheme", request.Scheme, visitor, ref visitorState );

            foreach ( KeyValuePair<string, StringValues> header in request.Headers )
            {
                this.Visit( header.Key, header.Value.ToString(), visitor, ref visitorState );
            }

        }
    }
```

You will also create a custom <xref:PostSharp.Patterns.Diagnostics.Custom.LogEventMetadata> if you want to define custom transactions. See <xref:custom-logging-transactions> for details. 

