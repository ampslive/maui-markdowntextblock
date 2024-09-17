// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Extensions.Tables;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyTable : IAddChild
{
    private Table _table;
    private MyTableUIElement _tableElement;

    public Element TextElement
    {
        get => _tableElement;
    }

    public MyTable(Table table)
    {
        _table = table;
        var row = table.FirstOrDefault() as TableRow;
        var column = row == null ? 0 : row.Count;

        _tableElement = new MyTableUIElement
        (
            column,
            table.Count,
            1,
            new SolidColorBrush(Colors.Gray)
        );
    }

    public void AddChild(IAddChild child)
    {
        if (child is MyTableCell cellChild)
        {
            var cell = cellChild.Container;

            Grid.SetColumn(cell, cellChild.ColumnIndex);
            Grid.SetRow(cell, cellChild.RowIndex);
            Grid.SetColumnSpan(cell, cellChild.ColumnSpan);
            Grid.SetRowSpan(cell, cellChild.RowSpan);

            _tableElement.Children.Add(cell);
        }
    }
}
