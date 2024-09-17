// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Syntax;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyBlockContainer : IAddChild
{
    private ContainerBlock _containerBlock;
    private MyFlowDocument _flowDocument;

    public Element TextElement
    {
        get => _flowDocument.Root;
    }

    public MyBlockContainer(ContainerBlock containerBlock)
    {
        _containerBlock = containerBlock;
        _flowDocument = new MyFlowDocument(containerBlock);
    }

    public void AddChild(IAddChild child)
    {
        _flowDocument.AddChild(child);
    }
}
