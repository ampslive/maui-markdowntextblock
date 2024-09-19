// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HtmlAgilityPack;
using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyHeading : IAddChild
{
    private Label _paragraph;
    private HeadingBlock? _headingBlock;
    private HtmlNode? _htmlNode;
    private MarkdownConfig _config;

    public bool IsHtml => _htmlNode != null;

    public Element TextElement
    {
        get => _paragraph;
    }

    public MyHeading(HeadingBlock headingBlock, MarkdownConfig config)
    {
        _headingBlock = headingBlock;
        _paragraph = new Label { FormattedText = new FormattedString() };
        _config = config;

        var level = headingBlock.Level;
        _paragraph.FontSize = level switch
        {
            1 => _config.Themes.H1FontSize,
            2 => _config.Themes.H2FontSize,
            3 => _config.Themes.H3FontSize,
            4 => _config.Themes.H4FontSize,
            5 => _config.Themes.H5FontSize,
            _ => _config.Themes.H6FontSize,
        };
        _paragraph.TextColor = _config.Themes.HeadingForeground;
        _paragraph.FontAttributes = level switch
        {
            1 => _config.Themes.H1FontWeight,
            2 => _config.Themes.H2FontWeight,
            3 => _config.Themes.H3FontWeight,
            4 => _config.Themes.H4FontWeight,
            5 => _config.Themes.H5FontWeight,
            _ => _config.Themes.H6FontWeight,
        };
        _paragraph.Padding = _config.Themes.Padding;
        _paragraph.Margin = _config.Themes.InternalMargin;
    }

    public MyHeading(HtmlNode htmlNode, MarkdownConfig config)
    {
        _htmlNode = htmlNode;
        _paragraph = new Label { FormattedText = new FormattedString() };
        _config = config;

        var align = _htmlNode.GetAttributeValue("align", "left");
        _paragraph.HorizontalTextAlignment = align switch
        {
            "left" => TextAlignment.Start,
            "right" => TextAlignment.End,
            "center" => TextAlignment.Center,
            //TODO: "justify" => TextAlignment.Justify,
            _ => TextAlignment.Start,
        };

        var level = int.Parse(htmlNode.Name.Substring(1));
        _paragraph.FontSize = level switch
        {
            1 => _config.Themes.H1FontSize,
            2 => _config.Themes.H2FontSize,
            3 => _config.Themes.H3FontSize,
            4 => _config.Themes.H4FontSize,
            5 => _config.Themes.H5FontSize,
            _ => _config.Themes.H6FontSize,
        };
        _paragraph.TextColor = _config.Themes.HeadingForeground;
        _paragraph.FontAttributes = level switch
        {
            1 => _config.Themes.H1FontWeight,
            2 => _config.Themes.H2FontWeight,
            3 => _config.Themes.H3FontWeight,
            4 => _config.Themes.H4FontWeight,
            5 => _config.Themes.H5FontWeight,
            _ => _config.Themes.H6FontWeight,
        };
    }

    public void AddChild(IAddChild child)
    {
        if (child.TextElement is FormattedString formattedChild)
        {
            foreach (var span in formattedChild.Spans)
            {
                _paragraph.FormattedText.Spans.Add(span);
            }
        }
        else if (child.TextElement is Span inlineChild)
        {
            _paragraph.FormattedText.Spans.Add(inlineChild);
        }
    }
}
