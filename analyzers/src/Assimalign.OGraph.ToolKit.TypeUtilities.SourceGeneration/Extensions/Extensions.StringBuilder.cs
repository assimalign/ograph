using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.ToolKit.TypeUtilities.SourceGeneration;

internal static class StringBuilderExtensions
{
    public static StringBuilder AppendTabs(this StringBuilder builder, ushort tabs)
    {
        for (int i = 0; i < tabs; i++)
        {
            builder.Append("\t");
        }

        return builder;
    }

    public static StringBuilder AppendTabbedLine(this StringBuilder builder, ushort tabs, string value)
    {
        var lines = value.Split(new[] { "\r\n" }, StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            builder.AppendTabs(tabs).AppendLine(line);
        }

        return builder;
    }
}
