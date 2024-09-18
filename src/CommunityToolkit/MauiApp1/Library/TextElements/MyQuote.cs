// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyQuote : IAddChild
{
    private QuoteBlock _quoteBlock;
    private MyFlowDocument _flowDocument;
    private Grid _paragraph;

    public Element TextElement
    {
        get => _paragraph;
    }

    public MyQuote(QuoteBlock quoteBlock)
    {
        _quoteBlock = quoteBlock;
        _flowDocument = new MyFlowDocument(quoteBlock);

        _paragraph = new Grid();
        _paragraph.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
        _paragraph.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
        _paragraph.Margin = new Thickness(0, 2, 0, 2);

        var bar = new Grid();
        bar.WidthRequest = 4;
        bar.Background = new SolidColorBrush(Colors.Gray);
        Grid.SetColumn(bar, 0);
        bar.VerticalOptions = LayoutOptions.Fill;
        bar.HorizontalOptions = LayoutOptions.Start;
        bar.Margin = new Thickness(0, 0, 4, 0);
        _paragraph.Children.Add(bar);

        var rightGrid = new Grid();
        rightGrid.Padding = new Thickness(4);
        rightGrid.Children.Add(_flowDocument.Root);
        Grid.SetColumn(rightGrid, 1);
        _paragraph.Children.Add(rightGrid);
    }

    public void AddChild(IAddChild child)
    {
        _flowDocument.AddChild(child);
    }
}
