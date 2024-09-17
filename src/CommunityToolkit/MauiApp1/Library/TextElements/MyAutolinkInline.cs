// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyAutolinkInline : IAddChild
{
    private AutolinkInline _autoLinkInline;
    private Command _command;
    private FormattedString _string;

    public Element TextElement => _string;

    public MyAutolinkInline(AutolinkInline autoLinkInline, string url)
    {
        _autoLinkInline = autoLinkInline;
        _command = new Command(async () => await Launcher.Default.TryOpenAsync(new Uri(url)));
        _string = new FormattedString();
    }

    public void AddChild(IAddChild child)
    {
        if (child.TextElement is FormattedString childString)
        {
            foreach (var childSpan in childString.Spans)
            {
                AddAsLink(childSpan);
            }
        }
        else if (child.TextElement is Span childSpan)
        {
            AddAsLink(childSpan);
        }
        else
        {
            //throw new Exception($"Error adding child to MyAutolinkInline: {child}");
        }
    }

    private void AddAsLink(Span span)
    {
        span.GestureRecognizers.Add(new TapGestureRecognizer { Command = _command });
        span.TextColor = Colors.Blue;
        _string.Spans.Add(span);
    }
}
