// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Labs.WinUI.MarkdownTextBlock;

namespace CommunityToolkit.WinUI.Controls.MarkdownTextBlockRns;

public sealed class MarkdownThemes : BindableObject
{
    internal static MarkdownThemes Default { get; } = new();

    public Thickness Padding { get; set; } = new(8);

    public Thickness InternalMargin { get; set; } = new(4);

    public CornerRadius CornerRadius { get; set; } = new(4);

    public double H1FontSize { get; set; } = 22;

    public double H2FontSize { get; set; } = 20;

    public double H3FontSize { get; set; } = 18;

    public double H4FontSize { get; set; } = 16;

    public double H5FontSize { get; set; } = 14;

    public double H6FontSize { get; set; } = 12;

    public Color HeadingForeground { get; set; } =Colors.Purple;

    public FontAttributes H1FontWeight { get; set; } = FontAttributes.Bold;

    public FontAttributes H2FontWeight { get; set; } = FontAttributes.None;

    public FontAttributes H3FontWeight { get; set; } = FontAttributes.None;

    public FontAttributes H4FontWeight { get; set;} = FontAttributes.None;

    public FontAttributes H5FontWeight { get; set; } = FontAttributes.None;

    public FontAttributes H6FontWeight { get; set; } = FontAttributes.None;

    public Color InlineCodeBackground { get; set; } = Colors.LightGray;

    public Color InlineCodeBorderBrush { get; set; } = Colors.Gray;

    public Thickness InlineCodeBorderThickness { get; set; } = new (1);

    public CornerRadius InlineCodeCornerRadius { get; set; } = new (2);

    public Thickness InlineCodePadding { get; set; } = new(0);

    public double InlineCodeFontSize { get; set; } = 10;

    public FontAttributes InlineCodeFontWeight { get; set; } = FontAttributes.None;
}
