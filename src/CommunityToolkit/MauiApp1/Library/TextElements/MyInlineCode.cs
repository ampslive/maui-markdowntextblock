// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyInlineCode : IAddChild
{
    private CodeInline _codeInline;
    private Span _inlineContainer;
    private MarkdownConfig _config;

    public Element TextElement
    {
        get => _inlineContainer;
    }

    public MyInlineCode(CodeInline codeInline, MarkdownConfig config)
    {
        _codeInline = codeInline;
        _config = config;
        _inlineContainer = new Span();
        _inlineContainer.BackgroundColor = _config.Themes.InlineCodeBackground;
        //_inlineContainer.BorderBrush = _config.Themes.InlineCodeBorderBrush;
        //_inlineContainer.BorderThickness = _config.Themes.InlineCodeBorderThickness;
        //_inlineContainer.CornerRadius = _config.Themes.InlineCodeCornerRadius;
        //_inlineContainer.Padding = _config.Themes.InlineCodePadding;
        _inlineContainer.FontSize = _config.Themes.InlineCodeFontSize;
        _inlineContainer.FontAttributes = _config.Themes.InlineCodeFontWeight;
        _inlineContainer.Text = codeInline.Content.ToString();
    }


    public void AddChild(IAddChild child) { }
}
