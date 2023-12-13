---
uid: logging-troubleshooting
---

# Troubleshooting logging

If you think you have followed all steps but records don't flow to your log, consider the following steps.


## Step 1. Does PostSharp Logging enhances your code?

The first thing to control is whether PostSharp Logging does it work at compile time.

1. Compile your project.
2. Open the project binary using a decompiler such as ILSpy.
3. Go to a method that you are sure must be logged.
4. Check the decompiled code. Does it include PostSharp-generated logging code, or is it just more or less your source code?

If the decompiled code does _not_ include PostSharp-generated logging code, check the following:

* Is the `PostSharp.Patterns.Diagnostics` correctly installed in the project? The `PostSharp.Patterns.Diagnostics.Redist` package is not enough!
* Are you sure that the `SkipPostSharp` MSBuild property is *not* set for this project?
* Are you sure that your multicast attributes are correct? To be sure, explicitly use the `[Log]` attribute on a select method and test again.

## Step 2. Check the run-time setup

If the previous step was succesfull, i.e. if you can see PostSharp-generated logging code, but you still can't see any record in the log, then the problem may be in the run-time configuration.

* Verify that you have respected the instructions to set up the specific logging backend. See <xref:backends> for details.
* Verify that the verbosity level in your logging backend is set to a low enough value. Try to enable the most detailed verbosity level and only reduce it after you see that logging is working.
* Verify that the verbosity level in PostSharp Logging is set to a high enough value. For details, see <xref:log-enabling>.