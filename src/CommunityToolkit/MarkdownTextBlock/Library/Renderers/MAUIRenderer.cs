using CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;
using Markdig.Renderers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownTextBlock.Library.Renderers;

public class MAUIRenderer : RendererBase
{
    public StackLayout StackLayout { get; private set; }

    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);
        return StackLayout ?? new();
    }
}
