// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyLineBreak : IAddChild
{
    private Span _lineBreak;

    public Element TextElement
    {
        get => _lineBreak;
    }

    public MyLineBreak()
    {
        _lineBreak = new Span() { Text = Environment.NewLine };
    }

    public void AddChild(IAddChild child) { }
}
