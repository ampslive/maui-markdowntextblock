using Markdig;
using Markdig.Syntax.Inlines;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Dispatching;
using System.Collections.ObjectModel;

namespace MarkDownTextBlock.Core;

public class MarkdownView : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(MarkdownView), propertyChanged: OnTextChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MarkdownView)bindable;
        control.RenderMarkdown((string)newValue);
    }

    private void RenderMarkdown(string markdown)
    {
        var document = Markdown.Parse(markdown);
        var stackLayout = new StackLayout();

        foreach (var block in document)
        {
            if (block is Markdig.Syntax.ParagraphBlock paragraph)
            {
                var paragraphLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                foreach (var inline in paragraph.Inline)
                {
                    if (inline is Markdig.Syntax.Inlines.LiteralInline literal)
                    {
                        paragraphLayout.Children.Add(new Label { Text = literal.Content.ToString() });
                    }
                    else if (inline is Markdig.Syntax.Inlines.EmphasisInline emphasis)
                    {
                        var text = string.Join("", emphasis.Select(i => i.ToString()));
                        paragraphLayout.Children.Add(new Label
                        {
                            Text = text,
                            FontAttributes = emphasis.DelimiterCount == 2 ? FontAttributes.Bold : FontAttributes.Italic
                        });
                    }
                    else if (inline is Markdig.Syntax.Inlines.LinkInline link)
                    {
                        if (link.IsImage)
                        {
                            var image = new Image { Source = link.Url };
                            paragraphLayout.Children.Add(image);
                        }
                        else
                        {
                            var linkLabel = new Label
                            {
                                Text = link.FirstChild.ToString(),
                                TextColor = Colors.Blue
                            };
                            var tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += (s, e) => MainThread.BeginInvokeOnMainThread(() =>
                            {
                                Launcher.OpenAsync(new Uri(link.Url));
                            });
                            linkLabel.GestureRecognizers.Add(tapGestureRecognizer);
                            paragraphLayout.Children.Add(linkLabel);
                        }
                    }
                }
                stackLayout.Children.Add(paragraphLayout);
            }
            else if (block is Markdig.Syntax.HeadingBlock heading)
            {
                stackLayout.Children.Add(new Label
                {
                    Text = heading.Inline.FirstChild.ToString(),
                    FontSize = heading.Level switch
                    {
                        1 => 24,
                        2 => 20,
                        3 => 18,
                        _ => 16
                    },
                    FontAttributes = FontAttributes.Bold
                });
            }
        }

        Content = stackLayout;
    }
}
