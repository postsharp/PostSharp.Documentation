// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Aspects;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends;
using PostSharp.Patterns.Caching.Backends.Azure;
using PostSharp.Patterns.Caching.Backends.Redis;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Adapters.AspNetCore;
using PostSharp.Patterns.Diagnostics.Adapters.DiagnosticSource;
using PostSharp.Patterns.Diagnostics.Adapters.HttpClient;
using PostSharp.Patterns.Diagnostics.Backends.ApplicationInsights;
using PostSharp.Patterns.Diagnostics.Backends.CommonLogging;
using PostSharp.Patterns.Diagnostics.Backends.Log4Net;
using PostSharp.Patterns.Diagnostics.Backends.Microsoft;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using PostSharp.Patterns.Diagnostics.Backends.Trace;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;

#if NETFRAMEWORK
using PostSharp.Patterns.Diagnostics.Adapters.AspNetFramework;
#endif

Console.WriteLine( "Hello, World!" );

// // This is to make sure that all packages are properly referenced.
_ = new Type[]
{
    typeof(Aspect), // PostSharp.Redist
    typeof(AggregatableAttribute), // PostSharp.Patterns.Aggregation.Redist
    typeof(CacheAttribute), // PostSharp.Patterns.Caching.Redist
    typeof(AzureServiceBusCacheInvalidator), // PostSharp.Patterns.Caching.Azure
    typeof(MemoryCacheBackend), // PostSharp.Patterns.Caching.IMemoryCache
    typeof(RedisCachingBackend), // PostSharp.Patterns.Caching.Redis
    typeof(RequiredAttribute), // PostSharp.Patterns.Common.Redist
    typeof(LogAttribute), // PostSharp.Patterns.Diagnostics.Redist
    typeof(ApplicationInsightsLoggingBackend), // PostSharp.Patterns.Diagnostics.ApplicationInsights
    typeof(AspNetCoreLogging), // PostSharp.Patterns.Diagnostics.AspNetCore
    typeof(CommonLoggingLoggingBackend), // PostSharp.Patterns.Diagnostics.CommonLogging
    typeof(DiagnosticSourceCollectingListener), // PostSharp.Patterns.Diagnostics.DiagnosticSource
    typeof(HttpClientLogging), // PostSharp.Patterns.Diagnostics.HttpClient
    typeof(LoggingConfigurationManager), // PostSharp.Patterns.Diagnostics.Configuration
    typeof(Log4NetLoggingBackend), // PostSharp.Patterns.Diagnostics.Log4Net
    typeof(MicrosoftLoggingBackend), // PostSharp.Patterns.Diagnostics.Microsoft
    typeof(NLogLoggingBackend), // PostSharp.Patterns.Diagnostics.NLog
    typeof(SerilogLoggingBackend), // PostSharp.Patterns.Diagnostics.Serilog
    typeof(TraceLoggingBackend), // PostSharp.Patterns.Diagnostics.Tracing
    typeof(NotifyPropertyChangedAttribute), // PostSharp.Patterns.Model.Redist
    typeof(CommandAttribute), // PostSharp.Patterns.Xaml.Redist

#if NETFRAMEWORK
    typeof(AspNetFrameworkRequestMetadata), // PostSharp.Patterns.Diagnostics.AspNetFramework
#endif
};