// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyEmphasisInline : IAddChild
{
    private FormattedString _formatted;
    private EmphasisInline _markdownObject;

    private bool _isBold;
    private bool _isItalic;
    private bool _isStrikeThrough;

    public Element TextElement
    {
        get => _formatted;
    }

    public MyEmphasisInline(EmphasisInline emphasisInline)
    {
        _formatted = new FormattedString();
        _markdownObject = emphasisInline;
    }

    public void AddChild(IAddChild child)
    {
        try
        {
            if (child.TextElement is Span span)
            {
                _formatted.Spans.Add(span);
            }
            else if (child.TextElement is FormattedString formattedString)
            {
                foreach (var formatted in formattedString.Spans)
                {
                    _formatted.Spans.Add(formatted);
                }
            }

            if (_isBold) { SetBold(); }
            if (_isItalic) { SetItalic(); }
            if (_isStrikeThrough) { SetStrikeThrough(); }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in {nameof(MyEmphasisInline)}.{nameof(AddChild)}: {ex.Message}");
        }
    }

    public void SetBold()
    {
        foreach (var span in _formatted.Spans)
        {
            span.FontAttributes |= FontAttributes.Bold;
        }

        _isBold = true;
    }

    public void SetItalic()
    {
        foreach (var span in _formatted.Spans)
        {
            span.FontAttributes |= FontAttributes.Italic;
        }

        _isItalic = true;
    }

    public void SetStrikeThrough()
    {
        foreach (var span in _formatted.Spans)
        {
            span.TextDecorations |= TextDecorations.Strikethrough;
        }

        _isStrikeThrough = true;
    }

    public void SetSubscript()
    {
        // TODO: _formatted.SetValue(Typography.VariantsProperty, FontVariants.Subscript);
    }

    public void SetSuperscript()
    {
        // TODO: _formatted.SetValue(Typography.VariantsProperty, FontVariants.Superscript);
    }
}
