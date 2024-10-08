// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Extensions.Tables;
using CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.Renderers.ObjectRenderers.Extensions;

public class TableRenderer : VisualElementMarkdownObjectRenderer<Table>
{
    protected override void Write(VisualElementRenderer renderer, Table table)
    {
        if (renderer == null) throw new ArgumentNullException(nameof(renderer));
        if (table == null) throw new ArgumentNullException(nameof(table));

        var myTable = new MyTable(table);

        renderer.Push(myTable);

        for (var rowIndex = 0; rowIndex < table.Count; rowIndex++)
        {
            var rowObj = table[rowIndex];
            var row = (TableRow)rowObj;

            for (var i = 0; i < row.Count; i++)
            {
                var cellObj = row[i];
                var cell = (TableCell)cellObj;
                var textAlignment = TextAlignment.Start;

                var columnIndex = i;

                if (table.ColumnDefinitions.Count > 0)
                {
                    columnIndex = cell.ColumnIndex < 0 || cell.ColumnIndex >= table.ColumnDefinitions.Count
                        ? i
                        : cell.ColumnIndex;
                    columnIndex = columnIndex >= table.ColumnDefinitions.Count ? table.ColumnDefinitions.Count - 1 : columnIndex;
                    var alignment = table.ColumnDefinitions[columnIndex].Alignment;
                    textAlignment = alignment switch
                    {
                        TableColumnAlign.Center => TextAlignment.Center,
                        TableColumnAlign.Left => TextAlignment.Start,
                        TableColumnAlign.Right => TextAlignment.End,
                        _ => TextAlignment.Start,
                    };
                }

                var myCell = new MyTableCell(cell, textAlignment, row.IsHeader, columnIndex, rowIndex);

                renderer.Push(myCell);
                renderer.Write(cell);
                renderer.Pop();
            }
        }

        renderer.Pop();
    }
}
