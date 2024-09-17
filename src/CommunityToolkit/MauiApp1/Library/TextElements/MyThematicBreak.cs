// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyThematicBreak : IAddChild
{
    private ThematicBreakBlock _thematicBreakBlock;
    private Border _border;

    public Element TextElement
    {
        get => _border;
    }

    public MyThematicBreak(ThematicBreakBlock thematicBreakBlock)
    {
        _thematicBreakBlock = thematicBreakBlock;
        _border = new Border();

        _border.WidthRequest = 500;
        _border.StrokeThickness = 1;
        _border.Margin = new Thickness(0, 4, 0, 4);
        _border.Stroke = new SolidColorBrush(Colors.Gray);
        _border.HeightRequest = 1;
        _border.HorizontalOptions = LayoutOptions.Start;
    }

    public void AddChild(IAddChild child) { }
}
