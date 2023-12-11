using System;
using System.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Web;

namespace Assimalign.OGraph;

[DebuggerDisplay("Path: /{ToString()}")]
public readonly struct Path : IEquatable<Path>, IEqualityComparer<Path>
{
    private readonly PathSegment[] segments;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Path(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        segments = GetSegments(HttpUtility.UrlDecode(path));
    }

    private PathSegment[] GetSegments(string path)
    {
        var index = 0;
        var segments = new PathSegment[10];
        var segment = string.Empty;

        for (int i = 0; i < path.Length; i++)
        {
            var character = path[i];

            // Check if we reached the end of the current segment
            if (character == '/' || character == '\\')
            {
                // Let's skip leading slashes
                if (i == 0) continue;

                segments[index] = new PathSegment(segment, index);
                index++;
                segment = string.Empty;

                // Lets see if we reached the buffer in the array. Resize if reached.
                if (index == segments.Length)
                {
                    Array.Resize(ref segments, 5);
                }
            }
            else if ((i + 1) >= path.Length)
            {
                segment = segment + character;
                segments[index] = new PathSegment(segment, index);
                index++;
                segment = string.Empty;
            }
            else
            {
                segment = segment + character;
            }
        }

        // Resizes segments to actual length
        Array.Resize(ref segments, index);

        return segments;
    }

    /// <summary>
    /// A collection of path segments.
    /// </summary>
    public PathSegment[] Segments
    {
        get
        {
            var copy = new PathSegment[segments.Length];
            segments.CopyTo(copy, 0);
            return copy;
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join("/", Segments.Select(s => s.Value));
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
            var incoming = path.Segments[i];
            var current = Segments[i];

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