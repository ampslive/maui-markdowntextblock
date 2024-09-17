// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Extensions.TaskLists;
using Microsoft.Maui.Controls.Shapes;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyTaskListCheckBox : IAddChild
{
    private TaskList _taskList;
    //private Border _grid;
    private Span _span;

    public Element TextElement => _span;

    public MyTaskListCheckBox(TaskList taskList)
    {
        _taskList = taskList;

        _span = new Span();
        // TODO, make this look nice with a real font, image or some inline glyph
        _span.Text = taskList.Checked ? "[ ] " : "[X] ";

        //_grid = new Border
        //{
        //    WidthRequest = 16,
        //    HeightRequest = 16,
        //    Margin = new Thickness(2, 0, 2, 0),
        //    StrokeThickness = 1,
        //    Stroke = new SolidColorBrush(Colors.Gray),
        //    Padding = new Thickness(0),
        //    StrokeShape = new RoundRectangle() { CornerRadius = 2 },
        //    HorizontalOptions = LayoutOptions.Start,
        //};

        //if (taskList.Checked)
        //{
        //    var icon = new Label
        //    {
        //        FontSize = 16,
        //        HorizontalOptions = LayoutOptions.Center,
        //        VerticalOptions = LayoutOptions.Center,
        //        Text = "\uE73E"
        //    };

        //    _grid.Content = icon;
        //}
    }

    public void AddChild(IAddChild child)
    {
    }
}
