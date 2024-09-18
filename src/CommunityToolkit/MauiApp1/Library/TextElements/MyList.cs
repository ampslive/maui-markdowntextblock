// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;
using RomanNumerals;
using System.Globalization;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyList : IAddChild
{
    private VerticalStackLayout _stackPanel;
    private ListBlock _listBlock;
    private BulletType _bulletType;
    private bool _isOrdered;
    private int _startIndex = 1;
    private int _index = 1;
    private const string _dot = "• ";

    public Element TextElement
    {
        get => _stackPanel;
    }

    public MyList(ListBlock listBlock)
    {
        _stackPanel = new VerticalStackLayout();
        _listBlock = listBlock;

        if (listBlock.IsOrdered)
        {
            _isOrdered = true;
            _bulletType = ToBulletType(listBlock.BulletType);

            if (listBlock.OrderedStart != null && (listBlock.DefaultOrderedStart != listBlock.OrderedStart))
            {
                _startIndex = int.Parse(listBlock.OrderedStart, NumberFormatInfo.InvariantInfo);
                _index = _startIndex;
            }
        }

    }

    public void AddChild(IAddChild child)
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

        var textBlock = new Label()
        {
            Text = GetBulletChar(),
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, 0, 4, 0),
        };
        Grid.SetColumn(textBlock, 0);
        grid.Children.Add(textBlock);

        var flowDoc = new MyFlowDocument();
        Grid.SetColumn(flowDoc.Root, 1);
        flowDoc.AddChild(child);
        flowDoc.Root.Padding = new Thickness(0);
        flowDoc.Root.VerticalOptions = LayoutOptions.Start;
        grid.Children.Add(flowDoc.Root);

        _stackPanel.Children.Add(grid);
    }

    private string GetBulletChar()
    {
        string bullet;
        if (_isOrdered)
        {
            bullet = _bulletType switch
            {
                BulletType.Number => $"{_index}. ",
                BulletType.LowerAlpha => $"{_index.ToAlphabetical()}. ",
                BulletType.UpperAlpha => $"{_index.ToAlphabetical().ToUpper()}. ",
                BulletType.LowerRoman => $"{_index.ToRomanNumerals().ToLower()} ",
                BulletType.UpperRoman => $"{_index.ToRomanNumerals().ToUpper()} ",
                BulletType.Circle => _dot,
                _ => _dot
            };
            _index++;
        }
        else
        {
            bullet = _dot;
        }

        return bullet;
    }

    private BulletType ToBulletType(char bullet)
    {
        /// Gets or sets the type of the bullet (e.g: '1', 'a', 'A', 'i', 'I').
        switch (bullet)
        {
            case '1':
                return BulletType.Number;
            case 'a':
                return BulletType.LowerAlpha;
            case 'A':
                return BulletType.UpperAlpha;
            case 'i':
                return BulletType.LowerRoman;
            case 'I':
                return BulletType.UpperRoman;
            default:
                return BulletType.Circle;
        }
    }
}

internal enum BulletType
{
    Circle,
    Number,
    LowerAlpha,
    UpperAlpha,
    LowerRoman,
    UpperRoman
}
