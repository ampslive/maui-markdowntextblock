// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Extensions.Tables;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyTableCell : IAddChild
{
    private TableCell _tableCell;
    private MyFlowDocument _flowDocument;
    private bool _isHeader;
    private int _columnIndex;
    private int _rowIndex;
    private Grid _container;

    public Element TextElement
    {
        get => _container;
    }

    public Grid Container
    {
        get => _container;
    }

    public int ColumnSpan
    {
        get => _tableCell.ColumnSpan;
    }

    public int RowSpan
    {
        get => _tableCell.RowSpan;
    }

    public int ColumnIndex
    {
        get => _columnIndex;
    }

    public int RowIndex
    {
        get => _rowIndex;
    }

    public MyTableCell(TableCell tableCell, TextAlignment textAlignment, bool isHeader, int columnIndex, int rowIndex)
    {
        _isHeader = isHeader;
        _tableCell = tableCell;
        _columnIndex = columnIndex;
        _rowIndex = rowIndex;
        _container = new Grid();

        _flowDocument = new MyFlowDocument(tableCell);
        _flowDocument.Root.HorizontalOptions = textAlignment switch
        {
            TextAlignment.Start => LayoutOptions.Start,
            TextAlignment.Center => LayoutOptions.Center,
            TextAlignment.End => LayoutOptions.End,
            _ => LayoutOptions.Start,
        };

        _container.Padding = new Thickness(4);
        if (_isHeader)
        {
            // TODO
            //_flowDocument.Root.FontWeight = FontWeights.Bold;
        }
        _flowDocument.Root.HorizontalOptions = textAlignment switch
        {
            TextAlignment.Start => LayoutOptions.Start,
            TextAlignment.Center => LayoutOptions.Center,
            TextAlignment.End => LayoutOptions.End,
            _ => LayoutOptions.Start,
        };
        _container.Children.Add(_flowDocument.Root);
    }

    public void AddChild(IAddChild child)
    {
        _flowDocument.AddChild(child);
    }
}
