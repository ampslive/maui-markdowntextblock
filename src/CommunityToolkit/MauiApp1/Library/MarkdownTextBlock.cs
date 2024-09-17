// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Labs.WinUI.MarkdownTextBlock.Renderers;
using CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;
using Markdig;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock;

public partial class MarkdownTextBlock : ContentView
{
    private const string MarkdownContainerName = "MarkdownContainer";
    private Grid? _container;
    private MarkdownPipeline _pipeline;
    private MyFlowDocument _document;
    private VisualElementRenderer _renderer;

    public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
        nameof(Config),
        typeof(MarkdownConfig),
        typeof(MarkdownTextBlock),
        null,
        propertyChanged: OnConfigChanged,
        coerceValue: (b, v) => v as MarkdownConfig ?? MarkdownConfig.Default,
        defaultValueCreator: b => MarkdownConfig.Default);

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(MarkdownTextBlock),
        null,
        propertyChanged: OnTextChanged);

    public MarkdownTextBlock()
    {
        _document = new MyFlowDocument();
        _pipeline = new MarkdownPipelineBuilder()
            .UseEmphasisExtras()
            .UseAutoLinks()
            .UseTaskLists()
            .UsePipeTables()
            .Build();

        _renderer ??= new VisualElementRenderer(_document, MarkdownConfig.Default);
        _pipeline.Setup(_renderer);

        _container = new Grid();
        _container.Children.Add(_document.Root);
        Content = _container;
    }

    public MarkdownConfig Config
    {
        get => (MarkdownConfig)GetValue(ConfigProperty);
        set => SetValue(ConfigProperty, value);
    }

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void OnConfigChanged(BindableObject d, object? oldValue, object? newValue)
    {
        if (d is MarkdownTextBlock self)
        {
            self.ApplyConfig(self.Config);
        }
    }

    private static void OnTextChanged(BindableObject d, object? oldValue, object? newValue)
    {
        if (d is MarkdownTextBlock self)
        {
            self.ApplyText(self.Text);
        }
    }

    private void ApplyConfig(MarkdownConfig config)
    {
        if (_renderer is not null)
        {
            _renderer.Config = config;
        }
    }

    private void ApplyText(string? text)
    {
        var markdown = Markdown.Parse(text ?? "", _pipeline);
        if (_renderer is not null)
        {
            _renderer.ReloadDocument();
            _renderer.Render(markdown);
        }
    }
}
