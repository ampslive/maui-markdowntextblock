// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyInlineText : IAddChild
{
    private Span _run;

    public Element TextElement
    {
        get => _run;
    }

    public MyInlineText(string text)
    {
        _run = new Span()
        {
            Text = text
        };
    }

    public void AddChild(IAddChild child) { }
}
