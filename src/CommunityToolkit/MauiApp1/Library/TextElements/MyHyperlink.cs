// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HtmlAgilityPack;
using Markdig.Syntax.Inlines;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyHyperlink : IAddChild
{
    private LinkInline? _linkInline;
    private HtmlNode? _htmlNode;
    private string? _baseUrl;
    private FormattedString _hyperlink = new();
    private Command _command;

    public bool IsHtml => _htmlNode != null;

    public Element TextElement
    {
        get => _hyperlink;
    }

    public MyHyperlink(LinkInline linkInline, string? baseUrl)
    {
        _linkInline = linkInline;
        _baseUrl = baseUrl;
        var url = linkInline.GetDynamicUrl != null ? linkInline.GetDynamicUrl() ?? linkInline.Url : linkInline.Url;
        var uri = MarkdownTextBlockExtensions.GetUri(url, baseUrl);
        _command = new Command(async () => await Launcher.Default.TryOpenAsync(uri));
    }

    public MyHyperlink(HtmlNode htmlNode, string? baseUrl)
    {
        _htmlNode = htmlNode;
        _baseUrl = baseUrl;
        var url = htmlNode.GetAttributeValue("href", "#");
        var uri = MarkdownTextBlockExtensions.GetUri(url, baseUrl);
        _command = new Command(async () => await Launcher.Default.TryOpenAsync(uri));
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
            //throw new Exception($"Error adding child to MyHyperlink: {child}");
        }
    }

    private void AddAsLink(Span span)
    {
        span.GestureRecognizers.Add(new TapGestureRecognizer { Command = _command });
        span.TextColor = Colors.Blue;
        _hyperlink.Spans.Add(span);
    }
}
