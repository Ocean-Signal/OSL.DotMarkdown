// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace DotMarkdown;

[DebuggerDisplay("NewLineHandling = {NewLineHandling} CloseOutput = {CloseOutput}")]
public class MarkdownWriterSettings
{
    public MarkdownWriterSettings(MarkdownFormat? format          = null,
        string?                                   newLineChars    = null,
        NewLineHandling                           newLineHandling = NewLineHandling.Replace,
        bool                                      closeOutput     = false,
        MarkdownCharEscaper?                      escapeStrategy  = null
    )
    {
        Format = format ?? MarkdownFormat.Default;
        NewLineChars = newLineChars ?? Environment.NewLine;
        NewLineHandling = newLineHandling;
        CloseOutput = closeOutput;
        Escaper = escapeStrategy ?? MarkdownCharEscaper.Default;
    }


    public static MarkdownWriterSettings Default { get; } = new();

    internal static MarkdownWriterSettings DefaultCloseOutput { get; } = Default.WithCloseOutput(true);

    internal static MarkdownWriterSettings Debugging { get; } = new(MarkdownFormat.Debugging);

    public MarkdownFormat Format { get; }

    public string NewLineChars { get; }

    public NewLineHandling NewLineHandling { get; }

    public bool CloseOutput { get; }

    public MarkdownCharEscaper Escaper { get; }


    public MarkdownWriterSettings WithFormat(MarkdownFormat format)
    {
        return new MarkdownWriterSettings(format, NewLineChars, NewLineHandling, CloseOutput);
    }

    public MarkdownWriterSettings WithNewLineChars(string newLineChars)
    {
        return new MarkdownWriterSettings(Format, newLineChars, NewLineHandling, CloseOutput);
    }

    public MarkdownWriterSettings WithNewLineHandling(NewLineHandling newLineHandling)
    {
        return new MarkdownWriterSettings(Format, NewLineChars, newLineHandling, CloseOutput);
    }

    public MarkdownWriterSettings WithCloseOutput(bool closeOutput)
    {
        return new MarkdownWriterSettings(Format, NewLineChars, NewLineHandling, closeOutput);
    }

    public MarkdownWriterSettings WithDefaultEscaper(MarkdownCharEscaper escaper) =>
        new(Format, NewLineChars, NewLineHandling, CloseOutput, escaper);

    internal static MarkdownWriterSettings From(MarkdownFormat? format)
    {
        if (format is null
            || ReferenceEquals(format, MarkdownFormat.Default))
        {
            return Default;
        }

        return new MarkdownWriterSettings(format);
    }
}
