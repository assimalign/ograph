using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.CodeAnalysis;

internal static class StringBuilderExtensions
{
    public static StringBuilder AppendTabs(this StringBuilder builder, uint tabs)
    {
        for (int i = 0; i < tabs; i++)
        {
            builder.Append("\t");
        }

        return builder;
    }

    public static StringBuilder AppendTabbedLine(this StringBuilder builder, uint tabs, string value)
    {
        return builder.AppendTabs(tabs).AppendLine(value);
    }



}
