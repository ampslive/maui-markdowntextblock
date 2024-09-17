// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyParagraph : IAddChild
{
    private ParagraphBlock _paragraphBlock;
    private VerticalStackLayout _paragraph;
    private Label? _textBlock;

    public Element TextElement
    {
        get => _paragraph;
    }

    public MyParagraph(ParagraphBlock paragraphBlock)
    {
        _paragraphBlock = paragraphBlock;
        _paragraph = new VerticalStackLayout();
    }

    public void AddChild(IAddChild child)
    {
        if (child.TextElement is VisualElement blockChild)
        {
            _textBlock = null;
            _paragraph.Children.Add(blockChild);
        }
        else if (child.TextElement is FormattedString formattedChild)
        {
            foreach (var span in formattedChild.Spans)
            {
                AddSpan(span);
            }
        }
        else if (child.TextElement is Span inlineChild)
        {
            AddSpan(inlineChild);
        }
        else
        {

        }
    }

    private void AddSpan(Span inlineChild)
    {
        if (_textBlock is null)
        {
            _textBlock = new Label();
            _textBlock.FormattedText = new FormattedString();
            _paragraph.Children.Add(_textBlock);
        }

        _textBlock.FormattedText.Spans.Add(inlineChild);
    }
}
