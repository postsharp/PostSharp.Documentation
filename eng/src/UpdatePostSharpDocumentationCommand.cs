// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using JetBrains.Annotations;
using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.BuildTools.Search.Backends;
using PostSharp.Engineering.BuildTools.Search.Updaters;

namespace BuildPostSharpDocumentation;

[UsedImplicitly]
internal class UpdatePostSharpDocumentationCommand : UpdateSearchCommandBase
{
    protected override CollectionUpdater CreateUpdater( SearchBackendBase backend ) =>
        new DocumentationUpdater<PostSharpDocCrawler>( ["PostSharp"], backend );
}