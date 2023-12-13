---
uid: threading-model-compatibility
title: "Compatibility of Threading Models"
product: "postsharp"
categories: "PostSharp;AOP;Metaprogramming"
summary: "The document presents a compatibility matrix showing which threading models can be used as children based on the parent model in PostSharp. Also, it explains how assignment between objects with threading models works."
---
# Compatibility of Threading Models

This table shows which threading models can you use as children based on the model of the parent.


## Compatibility Matrix

| Parent↓ Child→ | Actor | Freezable | Immutable | Private | Reader-Writer Synchronized | Synchronized | Thread Affine | Thread Unsafe |
|----------------|-------|-----------|-----------|---------|----------------------------|--------------|---------------|---------------|
| Actor | No | Yes | Yes | Yes | No | No | No | Yes |
| Freezable | No | Yes | Yes | Yes | No | No | No | No |
| Immutable | No | Yes | Yes | Yes | No | No | No | No |
| Reader-Writer Synchronized | Yes (Own) | Yes | Yes | Yes | Yes (Shared) | No | No | No |
| Synchronized | Yes (Own) | Yes | Yes | Yes | Yes (Shared) | Yes (Shared) | No | No |
| Thread Affine | Yes (Own) | Yes | Yes | Yes | No | No | Yes | Yes (Shared) |
| Thread Unsafe | Yes (Own) | Yes | Yes | Yes | No | No | Yes | Yes (Shared) |
| Private |  |  |  |  |  |  |  |  |

When you assign an object with a threading model to another object with a threading model as a child, the assignment statement will block until the thread has full access to both objects. For a Synchronized object, that means it has the lock. For a ReaderWriterSynchronized object, that means it has the write lock. The same is true for an assignment which removes the parent-child link from two objects.

## See Also

**Other Resources**

<xref:aggregatable>
<br>
