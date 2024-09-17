// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HtmlAgilityPack;
using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

public class MyFlowDocument : IAddChild
{
    private HtmlNode? _htmlNode;
    private VerticalStackLayout _stackPanel = new VerticalStackLayout();
    private MarkdownObject? _markdownObject;

    public Element TextElement => _stackPanel;

    public VerticalStackLayout Root => _stackPanel;

    public bool IsHtml => _htmlNode != null;

    public MyFlowDocument()
    {
    }

    public MyFlowDocument(MarkdownObject markdownObject)
    {
        _markdownObject = markdownObject;
    }

    public MyFlowDocument(HtmlNode node)
    {
        _htmlNode = node;
    }

    public void AddChild(IAddChild child)
    {
        var element = child.TextElement;
        if (element != null)
        {
            if (element is VisualElement uiElement)
            {
                _stackPanel.Children.Add(uiElement);
            }
            else if (element is FormattedString formattedInline)
            {
                var paragraph = new Label();
                paragraph.FormattedText = formattedInline;
                _stackPanel.Children.Add(paragraph);
            }
            else if (element is Span inline)
            {
                var paragraph = new Label();
                paragraph.FormattedText = new FormattedString
                {
                    Spans = { inline }
                };
                _stackPanel.Children.Add(paragraph);
            }
            else
            {

            }
        }
    }
}
