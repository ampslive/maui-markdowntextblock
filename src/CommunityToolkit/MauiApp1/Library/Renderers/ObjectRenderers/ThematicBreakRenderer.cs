// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;
using CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.Renderers.ObjectRenderers;

internal class ThematicBreakRenderer : VisualElementMarkdownObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(VisualElementRenderer renderer, ThematicBreakBlock obj)
    {
        if (renderer == null) throw new ArgumentNullException(nameof(renderer));
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var thematicBreak = new MyThematicBreak(obj);

        renderer.WriteBlock(thematicBreak);
    }
}
