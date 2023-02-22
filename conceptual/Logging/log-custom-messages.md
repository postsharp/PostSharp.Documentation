---
uid: log-custom-messages
title: "Writing Custom Messages"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
---
# Writing Custom Messages

This section describes how to write a standalone message using PostSharp Logging. Instead of a standalone message, you will often want to write a pair of messages to mark the beginning and the end (successful or not) of an activity. For this scenario, see <xref:log-custom-activities>. 

> [!IMPORTANT]
> All projects that write manual log records and activities should be processed by PostSharp, otherwise the execution of your application will be slower. Therefore your projects should have a reference to the *PostSharp.Patterns.Common* package (a reference to *PostSharp.Patterns.Common.Redist* is not sufficient), and PostSharp should not be disabled on this project. 


## Writing text messages

The simplest scenario is to write a constant string.


### To write a custom text message to the log:

1. Add the following statements on the top of your C# file:

    ```csharp
    using PostSharp.Patterns.Diagnostics;
                      using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;
    ```


2. Add and initialize a static read-only field of type <xref:PostSharp.Patterns.Diagnostics.LogSource> and initialize it with the <xref:PostSharp.Patterns.Diagnostics.LogSource.Get> method: 

    ```csharp
    private static readonly LogSource logSource = LogSource.Get();
    ```


3. Evaluate the property corresponding to the desired log level, e.g. <xref:PostSharp.Patterns.Diagnostics.LogSource.Debug> or <xref:PostSharp.Patterns.Diagnostics.LogSource.Error>. 


4. Invoke the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.Write``1(``0@,PostSharp.Patterns.Diagnostics.WriteMessageOptions@)> method. For the first method, construct the message object by invoking the <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> method. 

    ```csharp
    logSource.Debug.Write( Formatted( "Hello, world." ) );
    ```



### Example

```cs
using PostSharp.Patterns.Diagnostics;
    using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;
            
    static class Hasher
    {
        private static readonly LogSource logSource = LogSource.Get();

        public static async Task ReadAndHashAsync(string url)
        {
           if ( string.IsNullOrEmpty( url ) )
           {
              logSource.Warning.Write( Formatted( "Empty URL passed. Skipping this method." ));
              return;
           }
           
           // Details skipped.
        }
    }
```


## Writing text messages with parameters

Most of the time, you won't log constant strings, but you will want to include variable pieces of information. In this case, you can use one of the overloads of the <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> method that accepts formatting parameters. 

Note that the specification of the formatting string used by the <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> method is **not** identical to the one used by `string.Format`. The formatting string used by the <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> method is designed to support named parameters (for use with logging backends that support it, e.g. Serilog connected to Elastic Search) and for high-performance evaluation. 

The formatting string has the following specifications:

* Named parameters must be surrounded by curly brackets, e.g. `{MyParameter}`. 

* Values are matched to named parameters by position. This means that the order of named parameters in the formatting string must match the order of corresponding values passed to the <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> method and that two named parameters with the same name are not matched to the same value. 

* Anything inside a pair of curly brackets is considered as the parameter name and will be passed to the backend as is, without further parsing.

* Formatting specifiers are not supported but may be partially supported in the future. Do not use colons (`:`) in your parameter names, as they may be interpreted differently in future versions of PostSharp. 

* Use the escaped form of curly brackets `{{` and `}}` if you want to include curly brackets in the formatted string. 

> [!IMPORTANT]
> Even if you are not using a semantic backend, consider the performance impact of using `string.Format` or equivalent constructs such as interpolated strings. PostSharp Logging is highly optimized and is able to generate a logging record without allocating any memory on the heap. If you're using a high-performance backend, using `string.Format` can bring a significant performance overhead to your logging. 


### Example

```cs
using PostSharp.Patterns.Diagnostics;
    using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;              
    
    static class Hasher
    {
        private static readonly LogSource logSource = LogSource.Get();

        public static byte[] ReadAndHash(string url)
        {
            var hashAlgorithm = HashAlgorithm.Create("SHA256");
            hashAlgorithm.Initialize();

            var webClient = new WebClient();
            var buffer = new byte[16 * 1024];

            logSource.Info.Write( Formatted( "Using a {BufferSize}-byte buffer.", buffer.Length ) );

            using (var stream = webClient.OpenRead(url))
            {
                int countRead;
                while ((countRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    logSource.Info.Write( Formatted( "Got {CountRead} bytes.", countRead) );
                    hashAlgorithm.ComputeHash(buffer, 0, countRead);
                }
            }
            
            return hashAlgorithm.Hash;
        }
    }
```


## Writing semantic messages for easy statistical processing

One common scenario in production is to prioritize the resolution of warnings based on the number of occurrences. It often makes more sense to start working on a warning happening 10 times per second than on one happening once per day.

Unfortunately, with text-based messages like those produced by <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder>, this is cumbersome to achieve. The reason is that there is no message property to bucketize on. 

When statistical processing of messages is important to you, you should use, instead of formatted messages, semantic messages produced by the <xref:PostSharp.Patterns.Diagnostics.SemanticMessageBuilder> class. Semantic messages have a name and a list of properties, but no text. 

To emit semantic messages, use the <xref:PostSharp.Patterns.Diagnostics.SemanticMessageBuilder.Semantic*> method instead of <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*>. 


### Example

```cs
using PostSharp.Patterns.Diagnostics;
    using static PostSharp.Patterns.Diagnostics.SemanticMessageBuilder;              
    
    static class Hasher
    {
        private static readonly LogSource logSource = LogSource.Get();

        public static byte[] ReadAndHash(string url)
        {
            var hashAlgorithm = HashAlgorithm.Create("SHA256");
            hashAlgorithm.Initialize();

            var webClient = new WebClient();
            var buffer = new byte[16 * 1024];

            logSource.Info.Write( Semantic( "Initialize", ("BufferSize", buffer.Length ) ) );

            using (var stream = webClient.OpenRead(url))
            {
                int countRead;
                while ((countRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    logSource.Info.Write( Semantic( "ReadChunk", ("CountRead", countRead ) ) );
                    hashAlgorithm.ComputeHash(buffer, 0, countRead);
                }
            }
            
            return hashAlgorithm.Hash;
        }
    }
```


## Using default log levels

In the previous sections, we have explicitly specified the message severity by using the <xref:PostSharp.Patterns.Diagnostics.LogSource.Debug>, <xref:PostSharp.Patterns.Diagnostics.LogSource.Trace>, <xref:PostSharp.Patterns.Diagnostics.LogSource.Info>, <xref:PostSharp.Patterns.Diagnostics.LogSource.Warning>, <xref:PostSharp.Patterns.Diagnostics.LogSource.Error> and <xref:PostSharp.Patterns.Diagnostics.LogSource.Critical> properties. Instead of using these properties, you can use the <xref:PostSharp.Patterns.Diagnostics.LogSource.WithLevel(PostSharp.Patterns.Diagnostics.LogLevel)> method, which takes a parameter of type <xref:PostSharp.Patterns.Diagnostics.LogLevel>. 

Alternatively, you can use the <xref:PostSharp.Patterns.Diagnostics.LogSource.Default> and <xref:PostSharp.Patterns.Diagnostics.LogSource.Failure> properties. They resolve to the default level for success or failure messages. The default levels are <xref:PostSharp.Patterns.Diagnostics.LogSource.Debug> for <xref:PostSharp.Patterns.Diagnostics.LogSource.Default> and <xref:PostSharp.Patterns.Diagnostics.LogSource.Error> for <xref:PostSharp.Patterns.Diagnostics.LogSource.Failure>. The default levels can be configured with the <xref:PostSharp.Patterns.Diagnostics.LogSource.WithLevels(PostSharp.Patterns.Diagnostics.LogLevel,PostSharp.Patterns.Diagnostics.LogLevel)> method. This method allows you to configure the default levels from a central place. 


### To configure the default logging levels centrally:

1. Define an internal static field of type <xref:PostSharp.Patterns.Diagnostics.LogSource> for the prototype instance. Instantiate the prototype instance using <xref:PostSharp.Patterns.Diagnostics.LogSource.Get> and configure it using <xref:PostSharp.Patterns.Diagnostics.LogSource.WithLevels(PostSharp.Patterns.Diagnostics.LogLevel,PostSharp.Patterns.Diagnostics.LogLevel)>. 


2. For each type, clone the prototype using the <xref:PostSharp.Patterns.Diagnostics.LogSource.ForType(System.Type)> or <xref:PostSharp.Patterns.Diagnostics.LogSource.ForCurrentType> method. 



### Example

The following example illustrates how to configure log sources centrally:

```cs
using PostSharp.Patterns.Diagnostics;
      using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;              
    
      static class LogSources
      {
         // Configure a prototype LogSource that you will reuse in several classes.
         public static readonly LogSource Default = LogSource.Get().WithLevels( LogLevel.Trace, LogLevel.Warning );
      }
      
      class MyClass
      {
         // Instantiates a LogSource from the prototype for the current type.
         static readonly LogSource logSource = LogSources.Default.ForCurrentType();
         
         void MyMethod()
         {
            // Write a message with default verbosity.
            logSource.Default.Write( Formatted( "Hello, World." ) );
         }
      }
```


## Optimizing performance with the 'Elvis' operator

When you are writing a message, the first thing that the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.Write``1(``0@,PostSharp.Patterns.Diagnostics.WriteMessageOptions@)> method does is to check whether the requested logging level is enabled. If not, nothing else happens. If yes, the message is rendered and emitted. Altought this may seem fast enough for many scenarios, sometimes the cost of evaluating the parameters passed to <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*> and <xref:PostSharp.Patterns.Diagnostics.SemanticMessageBuilder.Semantic*> is prohibitive in itself. For these situations, it is preferable to skip the invocation of <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder.Formatted*>, <xref:PostSharp.Patterns.Diagnostics.SemanticMessageBuilder.Semantic*> and <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.Write``1(``0@,PostSharp.Patterns.Diagnostics.WriteMessageOptions@)> altogether if the current verbosity is insufficient. 

One way to conditionally emit a message is to use an `if` and test for the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.IsEnabled> property. 

However, it is much more convenient to use the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.EnabledOrNull> and the C# "Elvis" operator, e.g. as in the construct `logSource.Debug.EnabledOrNull?.Write`. 


### Example

The following example uses the <xref:PostSharp.Patterns.Diagnostics.Custom.LogLevelSource.EnabledOrNull> and the Elvis operator to make sure the expensive `File.GetLastWriteTime` method is evaluated only when debug-level logging is enabled. 

```cs
logSource.Debug.EnabledOrNull?.Write( Formatted( "The last change date of the file is {LastChangeDate}.", File.GetLastWriteTime("oh-my.txt")  ) );
```


## Implementing your own type of messages

If neither <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder> nor <xref:PostSharp.Patterns.Diagnostics.SemanticMessageBuilder> fit your needs, you can create your own type of messages. 

Messages are types (preferably value types) that implement the <xref:PostSharp.Patterns.Diagnostics.Custom.Messages.IMessage> interface. It has a single method, <xref:PostSharp.Patterns.Diagnostics.Custom.Messages.IMessage.Write(PostSharp.Patterns.Diagnostics.Custom.ICustomLogRecordBuilder,PostSharp.Patterns.Diagnostics.Custom.CustomLogRecordItem)>, which should render the message into an <xref:PostSharp.Patterns.Diagnostics.Custom.ICustomLogRecordBuilder>. 

You can use the <xref:PostSharp.Patterns.Diagnostics.Custom.FormattingStringParser> type if you want to reuse the same formatting string syntax as the one used by <xref:PostSharp.Patterns.Diagnostics.FormattedMessageBuilder>. 

## See Also

**Other Resources**

<xref:log-properties>
<br>