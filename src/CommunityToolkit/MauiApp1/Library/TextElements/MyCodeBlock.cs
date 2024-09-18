// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;
using Microsoft.Maui.Controls.Shapes;
using System.Text;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyCodeBlock : IAddChild
{
    private CodeBlock _codeBlock;
    private Grid _paragraph;
    private MarkdownConfig _config;

    public Element TextElement
    {
        get => _paragraph;
    }

    public MyCodeBlock(CodeBlock codeBlock, MarkdownConfig config)
    {
        _codeBlock = codeBlock;
        _config = config;
        _paragraph = new Grid();
        var border = new Border();
        border.Background = _config.Themes.InlineCodeBackground;
        border.Padding = _config.Themes.Padding;
        border.Margin = _config.Themes.InternalMargin;
        border.StrokeShape = new RoundRectangle
        {
            CornerRadius = _config.Themes.CornerRadius
        };

        var codeLines = new Label();
        codeLines.FormattedText = new FormattedString();

        //            var formatter = new ColorCode.RichTextBlockFormatter(Extensions.GetOneDarkProStyle());
        var stringBuilder = new StringBuilder();

        // go through all the lines backwards and only add the lines to a stack if we have encountered the first non-empty line
        var lines = codeBlock.Lines.Lines;
        var stack = new Stack<string>();
        var encounteredFirstNonEmptyLine = false;
        if (lines != null)
        {
            for (var i = lines.Length - 1; i >= 0; i--)
            {
                var line = lines[i];
                if (String.IsNullOrWhiteSpace(line.ToString()) && !encounteredFirstNonEmptyLine)
                {
                    continue;
                }

                encounteredFirstNonEmptyLine = true;
                stack.Push(line.ToString());
            }

            // append all the lines in the stack to the string builder
            while (stack.Count > 0)
            {
                stringBuilder.AppendLine(stack.Pop());
            }
        }
        // TODO: real formatting
        codeLines.FormattedText.Spans.Add(new Span() { Text = stringBuilder.ToString().Trim() });
        //formatter.FormatRichTextBlock(stringBuilder.ToString(), fencedCodeBlock.ToLanguage(), richTextBlock);

        border.Content = codeLines;
        _paragraph.Children.Add(border);
    }

    public void AddChild(IAddChild child) { }
}
