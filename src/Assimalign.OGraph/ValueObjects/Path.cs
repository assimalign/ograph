using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace Assimalign.OGraph;

public readonly struct Path : IEquatable<Path>, IEqualityComparer<Path>
{
    private const string invalidCharacters = @"<>*%&:\?";

    public Path(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        if (path.Any(c => invalidCharacters.Contains(c)))
        {
            throw new ArgumentException($"The following path: '{path}' contains an invalid character. Disallowed Characters '{invalidCharacters}'.");
        }
        Segments = path.Trim('/').Split('/').Select(segment =>
        {
            return new PathSegment(segment);

        }).ToArray();
    }

    /// <summary>
    /// A collection of path segments.
    /// </summary>
    public PathSegment[] Segments { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join("/", Segments.Select(s=>s.Value));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var segment in Segments)
        {
            hashCode.Add(segment);
        }
        return hashCode.ToHashCode();
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? instance)
    {
        if (instance is Path path)
        {
            return Equals(path);
        }

        return false;
    }

    /// <inheritdoc />
    public bool Equals(Path path)
    {
        if (path.Segments.Length != Segments.Length)
        {
            return false;
        }
        for (int i = 0; i < Segments.Length; i++)
        {
            var incoming    = path.Segments[i];
            var current     = Segments[i];

            if (incoming.Value != current.Value)
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc />
    public bool Equals(Path left, Path right) => left.Equals(right);
    /// <inheritdoc />
    public int GetHashCode([DisallowNull] Path path)
    {
        return path.GetHashCode();
    }



    public static implicit operator Path(string path) => new Path(path);
    public static implicit operator string(Path path) => path.ToString();
}
