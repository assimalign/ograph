using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Assimalign.OGraph;

public readonly partial struct HeaderValue :
    IList<string?>,
    IReadOnlyList<string?>,
    IEquatable<HeaderValue>,
    IEquatable<string?>,
    IEquatable<string?[]?>
{
    /// <summary>
    /// A readonly instance of the <see cref="HeaderValue"/> struct whose value is an empty string array.
    /// </summary>
    /// <remarks>
    /// In application code, this field is most commonly used to safely represent a <see cref="HeaderValue"/> that has null string values.
    /// </remarks>
    public static readonly HeaderValue Empty = new HeaderValue(Array.Empty<string>());

    private readonly object? values;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderValue"/> structure using the specified string.
    /// </summary>
    /// <param name="value">A string value or <c>null</c>.</param>
    public HeaderValue(string? value)
    {
        values = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderValue"/> structure using the specified array of strings.
    /// </summary>
    /// <param name="values">A string array.</param>
    public HeaderValue(string?[]? values)
    {
        this.values = values;
    }

    /// <summary>
    /// Gets the number of <see cref="string"/> elements contained in this <see cref="HeaderValue" />.
    /// </summary>
    public int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
            object? value = values;
            if (value is null)
            {
                return 0;
            }
            if (value is string)
            {
                return 1;
            }
            else
            {
                // Not string, not null, can only be string[]
                return Unsafe.As<string?[]>(value).Length;
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Value
    {
        get
        {
            if (values is string str)
            {
                return str;
            }
            if (values is string[] strArr)
            {
                return string.Join(';', strArr);
            }

            return null;
        }
    }

    bool ICollection<string?>.IsReadOnly => true;

    /// <summary>
    /// Defines an implicit conversion of a given string to a <see cref="HeaderValue"/>.
    /// </summary>
    /// <param name="value">A string to implicitly convert.</param>
    public static implicit operator HeaderValue(string? value)
    {
        return new HeaderValue(value);
    }

    /// <summary>
    /// Defines an implicit conversion of a given string array to a <see cref="HeaderValue"/>.
    /// </summary>
    /// <param name="values">A string array to implicitly convert.</param>
    public static implicit operator HeaderValue(string?[]? values)
    {
        return new HeaderValue(values);
    }

    /// <summary>
    /// Defines an implicit conversion of a given <see cref="HeaderValue"/> to a string, with multiple values joined as a comma separated string.
    /// </summary>
    /// <remarks>
    /// Returns <c>null</c> where <see cref="HeaderValue"/> has been initialized from an empty string array or is <see cref="HeaderValue.Empty"/>.
    /// </remarks>
    /// <param name="values">A <see cref="HeaderValue"/> to implicitly convert.</param>
    public static implicit operator string?(HeaderValue values)
    {
        return values.GetStringValue();
    }

    /// <summary>
    /// Defines an implicit conversion of a given <see cref="HeaderValue"/> to a string array.
    /// </summary>
    /// <param name="value">A <see cref="HeaderValue"/> to implicitly convert.</param>
    public static implicit operator string?[]?(HeaderValue value)
    {
        return value.GetArrayValue();
    }

    

    

    /// <summary>
    /// Gets the <see cref="string"/> at index.
    /// </summary>
    /// <value>The string at the specified index.</value>
    /// <param name="index">The zero-based index of the element to get.</param>
    /// <exception cref="NotSupportedException">Set operations are not supported on readonly <see cref="HeaderValue"/>.</exception>
    string? IList<string?>.this[int index]
    {
        get => this[index];
        set => throw new NotSupportedException();
    }

    /// <summary>
    /// Gets the <see cref="string"/> at index.
    /// </summary>
    /// <value>The string at the specified index.</value>
    /// <param name="index">The zero-based index of the element to get.</param>
    public string? this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
            object? value = values;
            if (value is string str)
            {
                if (index == 0)
                {
                    return str;
                }
            }
            else if (value != null)
            {
                // Not string, not null, can only be string[]
                return Unsafe.As<string?[]>(value)[index]; // may throw
            }

            return OutOfBounds(); // throws
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string OutOfBounds()
    {
        return Array.Empty<string>()[0]; // throws
    }

    /// <summary>
    /// Converts the value of the current <see cref="HeaderValue"/> object to its equivalent string representation, with multiple values joined as a comma separated string.
    /// </summary>
    /// <returns>A string representation of the value of the current <see cref="HeaderValue"/> object.</returns>
    public override string ToString()
    {
        return GetStringValue() ?? string.Empty;
    }

    private string? GetStringValue()
    {
        // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
        object? value = values;
        if (value is string s)
        {
            return s;
        }
        else
        {
            return GetStringValueFromArray(value);
        }

        static string? GetStringValueFromArray(object? value)
        {
            if (value is null)
            {
                return null;
            }

            Debug.Assert(value is string[]);
            // value is not null or string, array, can only be string[]
            string?[] values = Unsafe.As<string?[]>(value);
            return values.Length switch
            {
                0 => null,
                1 => values[0],
                _ => GetJoinedStringValueFromArray(values),
            };
        }

        static string GetJoinedStringValueFromArray(string?[] values)
        {
            // Calculate final length
            int length = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string? value = values[i];
                // Skip null and empty values
                if (value != null && value.Length > 0)
                {
                    if (length > 0)
                    {
                        // Add separator
                        length++;
                    }

                    length += value.Length;
                }
            }
            // Create the new string
            return string.Create(length, values, (span, strings) => {
                int offset = 0;
                // Skip null and empty values
                for (int i = 0; i < strings.Length; i++)
                {
                    string? value = strings[i];
                    if (value != null && value.Length > 0)
                    {
                        if (offset > 0)
                        {
                            // Add separator
                            span[offset] = ',';
                            offset++;
                        }

                        value.AsSpan().CopyTo(span.Slice(offset));
                        offset += value.Length;
                    }
                }
            });
        }
    }

    /// <summary>
    /// Creates a string array from the current <see cref="HeaderValue"/> object.
    /// </summary>
    /// <returns>A string array represented by this instance.</returns>
    /// <remarks>
    /// <para>If the <see cref="HeaderValue"/> contains a single string internally, it is copied to a new array.</para>
    /// <para>If the <see cref="HeaderValue"/> contains an array internally it returns that array instance.</para>
    /// </remarks>
    public string?[] ToArray()
    {
        return GetArrayValue() ?? Array.Empty<string>();
    }

    private string?[]? GetArrayValue()
    {
        // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
        object? value = this.values;
        if (value is string[] values)
        {
            return values;
        }
        else if (value != null)
        {
            // value not array, can only be string
            return new[] { Unsafe.As<string>(value) };
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns the zero-based index of the first occurrence of an item in the <see cref="HeaderValue" />.
    /// </summary>
    /// <param name="item">The string to locate in the <see cref="HeaderValue"></see>.</param>
    /// <returns>the zero-based index of the first occurrence of <paramref name="item" /> within the <see cref="HeaderValue"></see>, if found; otherwise, -1.</returns>
    int IList<string?>.IndexOf(string? item)
    {
        return IndexOf(item);
    }

    private int IndexOf(string? item)
    {
        // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
        object? value = this.values;
        if (value is string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (string.Equals(values[i], item, StringComparison.Ordinal))
                {
                    return i;
                }
            }
            return -1;
        }

        if (value != null)
        {
            // value not array, can only be string
            return string.Equals(Unsafe.As<string>(value), item, StringComparison.Ordinal) ? 0 : -1;
        }

        return -1;
    }

    /// <summary>Determines whether a string is in the <see cref="HeaderValue" />.</summary>
    /// <param name="item">The <see cref="string"/> to locate in the <see cref="HeaderValue" />.</param>
    /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="HeaderValue" />; otherwise, false.</returns>
    bool ICollection<string?>.Contains(string? item)
    {
        return IndexOf(item) >= 0;
    }

    /// <summary>
    /// Copies the entire <see cref="HeaderValue" />to a string array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array" /> that is the destination of the elements copied from. The <see cref="Array" /> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
    /// <exception cref="ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
    /// <exception cref="ArgumentException">The number of elements in the source <see cref="HeaderValue"></see> is greater than the available space from <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.</exception>
    void ICollection<string?>.CopyTo(string?[] array, int arrayIndex)
    {
        CopyTo(array, arrayIndex);
    }

    private void CopyTo(string?[] array, int arrayIndex)
    {
        // Take local copy of values so type checks remain valid even if the StringValues is overwritten in memory
        object? value = this.values;
        if (value is string[] values)
        {
            Array.Copy(values, 0, array, arrayIndex, values.Length);
            return;
        }

        if (value != null)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }
            if (array.Length - arrayIndex < 1)
            {
                throw new ArgumentException(
                    $"'{nameof(array)}' is not long enough to copy all the items in the collection. Check '{nameof(arrayIndex)}' and '{nameof(array)}' length.");
            }

            // value not array, can only be string
            array[arrayIndex] = Unsafe.As<string>(value);
        }
    }

    void ICollection<string?>.Add(string? item) => throw new NotSupportedException();

    void IList<string?>.Insert(int index, string? item) => throw new NotSupportedException();

    bool ICollection<string?>.Remove(string? item) => throw new NotSupportedException();

    void IList<string?>.RemoveAt(int index) => throw new NotSupportedException();

    void ICollection<string?>.Clear() => throw new NotSupportedException();

    /// <summary>Retrieves an object that can iterate through the individual strings in this <see cref="HeaderValue" />.</summary>
    /// <returns>An enumerator that can be used to iterate through the <see cref="HeaderValue" />.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(values);
    }

    /// <inheritdoc cref="GetEnumerator()" />
    IEnumerator<string?> IEnumerable<string?>.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="GetEnumerator()" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Indicates whether the specified <see cref="HeaderValue"/> contains no string values.
    /// </summary>
    /// <param name="value">The <see cref="HeaderValue"/> to test.</param>
    /// <returns>true if <paramref name="value">value</paramref> contains a single null or empty string or an empty array; otherwise, false.</returns>
    public static bool IsNullOrEmpty(HeaderValue value)
    {
        object? data = value.values;
        if (data is null)
        {
            return true;
        }
        if (data is string[] values)
        {
            return values.Length switch
            {
                0 => true,
                1 => string.IsNullOrEmpty(values[0]),
                _ => false,
            };
        }
        else
        {
            // Not array, can only be string
            return string.IsNullOrEmpty(Unsafe.As<string>(data));
        }
    }

    /// <summary>
    /// Concatenates two specified instances of <see cref="HeaderValue"/>.
    /// </summary>
    /// <param name="values1">The first <see cref="HeaderValue"/> to concatenate.</param>
    /// <param name="values2">The second <see cref="HeaderValue"/> to concatenate.</param>
    /// <returns>The concatenation of <paramref name="values1"/> and <paramref name="values2"/>.</returns>
    public static HeaderValue Concat(HeaderValue values1, HeaderValue values2)
    {
        int count1 = values1.Count;
        int count2 = values2.Count;

        if (count1 == 0)
        {
            return values2;
        }

        if (count2 == 0)
        {
            return values1;
        }

        var combined = new string[count1 + count2];
        values1.CopyTo(combined, 0);
        values2.CopyTo(combined, count1);
        return new HeaderValue(combined);
    }

    /// <summary>
    /// Concatenates specified instance of <see cref="HeaderValue"/> with specified <see cref="string"/>.
    /// </summary>
    /// <param name="values">The <see cref="HeaderValue"/> to concatenate.</param>
    /// <param name="value">The <see cref="string" /> to concatenate.</param>
    /// <returns>The concatenation of <paramref name="values"/> and <paramref name="value"/>.</returns>
    public static HeaderValue Concat(in HeaderValue values, string? value)
    {
        if (value == null)
        {
            return values;
        }

        int count = values.Count;
        if (count == 0)
        {
            return new HeaderValue(value);
        }

        var combined = new string[count + 1];
        values.CopyTo(combined, 0);
        combined[count] = value;
        return new HeaderValue(combined);
    }

    /// <summary>
    /// Concatenates specified instance of <see cref="string"/> with specified <see cref="HeaderValue"/>.
    /// </summary>
    /// <param name="value">The <see cref="string" /> to concatenate.</param>
    /// <param name="values">The <see cref="HeaderValue"/> to concatenate.</param>
    /// <returns>The concatenation of <paramref name="values"/> and <paramref name="values"/>.</returns>
    public static HeaderValue Concat(string? value, in HeaderValue values)
    {
        if (value == null)
        {
            return values;
        }

        int count = values.Count;
        if (count == 0)
        {
            return new HeaderValue(value);
        }

        var combined = new string[count + 1];
        combined[0] = value;
        values.CopyTo(combined, 1);
        return new HeaderValue(combined);
    }

    /// <summary>
    /// Determines whether two specified <see cref="HeaderValue"/> objects have the same values in the same order.
    /// </summary>
    /// <param name="left">The first <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The second <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool Equals(HeaderValue left, HeaderValue right)
    {
        int count = left.Count;

        if (count != right.Count)
        {
            return false;
        }

        for (int i = 0; i < count; i++)
        {
            if (left[i] != right[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether two specified <see cref="HeaderValue"/> have the same values.
    /// </summary>
    /// <param name="left">The first <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The second <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(HeaderValue left, HeaderValue right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified <see cref="HeaderValue"/> have different values.
    /// </summary>
    /// <param name="left">The first <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The second <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HeaderValue left, HeaderValue right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="HeaderValue"/> object have the same values.
    /// </summary>
    /// <param name="other">The string to compare to this instance.</param>
    /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as the value of this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(HeaderValue other) => Equals(this, other);

    /// <summary>
    /// Determines whether the specified <see cref="string"/> and <see cref="HeaderValue"/> objects have the same values.
    /// </summary>
    /// <param name="left">The <see cref="string"/> to compare.</param>
    /// <param name="right">The <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="left"/> is <c>null</c>, the method returns <c>false</c>.</returns>
    public static bool Equals(string? left, HeaderValue right) => Equals(new HeaderValue(left), right);

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and <see cref="string"/> objects have the same values.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The <see cref="string"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>. If <paramref name="right"/> is <c>null</c>, the method returns <c>false</c>.</returns>
    public static bool Equals(HeaderValue left, string? right) => Equals(left, new HeaderValue(right));

    /// <summary>
    /// Determines whether this instance and a specified <see cref="string"/>, have the same value.
    /// </summary>
    /// <param name="other">The <see cref="string"/> to compare to this instance.</param>
    /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>. If <paramref name="other"/> is <c>null</c>, returns <c>false</c>.</returns>
    public bool Equals(string? other) => Equals(this, new HeaderValue(other));

    /// <summary>
    /// Determines whether the specified string array and <see cref="HeaderValue"/> objects have the same values.
    /// </summary>
    /// <param name="left">The string array to compare.</param>
    /// <param name="right">The <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool Equals(string?[]? left, HeaderValue right) => Equals(new HeaderValue(left), right);

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and string array objects have the same values.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The string array to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool Equals(HeaderValue left, string?[]? right) => Equals(left, new HeaderValue(right));

    /// <summary>
    /// Determines whether this instance and a specified string array have the same values.
    /// </summary>
    /// <param name="other">The string array to compare to this instance.</param>
    /// <returns><c>true</c> if the value of <paramref name="other"/> is the same as this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(string?[]? other) => Equals(this, new HeaderValue(other));

    /// <inheritdoc cref="Equals(HeaderValue, string)" />
    public static bool operator ==(HeaderValue left, string? right) => Equals(left, new HeaderValue(right));

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and <see cref="string"/> objects have different values.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The <see cref="string"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HeaderValue left, string? right) => !Equals(left, new HeaderValue(right));

    /// <inheritdoc cref="Equals(string, HeaderValue)" />
    public static bool operator ==(string? left, HeaderValue right) => Equals(new HeaderValue(left), right);

    /// <summary>
    /// Determines whether the specified <see cref="string"/> and <see cref="HeaderValue"/> objects have different values.
    /// </summary>
    /// <param name="left">The <see cref="string"/> to compare.</param>
    /// <param name="right">The <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(string left, HeaderValue right) => !Equals(new HeaderValue(left), right);

    /// <inheritdoc cref="Equals(HeaderValue, string[])" />
    public static bool operator ==(HeaderValue left, string?[]? right) => Equals(left, new HeaderValue(right));

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and string array have different values.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The string array to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HeaderValue left, string?[]? right) => !Equals(left, new HeaderValue(right));

    /// <inheritdoc cref="Equals(string[], HeaderValue)" />
    public static bool operator ==(string?[]? left, HeaderValue right) => Equals(new HeaderValue(left), right);

    /// <summary>
    /// Determines whether the specified string array and <see cref="HeaderValue"/> have different values.
    /// </summary>
    /// <param name="left">The string array to compare.</param>
    /// <param name="right">The <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(string?[]? left, HeaderValue right) => !Equals(new HeaderValue(left), right);

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and <see cref="object"/>, which must be a
    /// <see cref="HeaderValue"/>, <see cref="string"/>, or array of <see cref="string"/>, have the same value.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The <see cref="object"/> to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(HeaderValue left, object? right) => left.Equals(right);

    /// <summary>
    /// Determines whether the specified <see cref="HeaderValue"/> and <see cref="object"/>, which must be a
    /// <see cref="HeaderValue"/>, <see cref="string"/>, or array of <see cref="string"/>, have different values.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The <see cref="object"/> to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HeaderValue left, object? right) => !left.Equals(right);

    /// <summary>
    /// Determines whether the specified <see cref="object"/>, which must be a
    /// <see cref="HeaderValue"/>, <see cref="string"/>, or array of <see cref="string"/>, and specified <see cref="HeaderValue"/>,  have the same value.
    /// </summary>
    /// <param name="left">The <see cref="HeaderValue"/> to compare.</param>
    /// <param name="right">The <see cref="object"/> to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(object? left, HeaderValue right) => right.Equals(left);

    /// <summary>
    /// Determines whether the specified <see cref="object"/> and <see cref="HeaderValue"/> object have the same values.
    /// </summary>
    /// <param name="left">The <see cref="object"/> to compare.</param>
    /// <param name="right">The <see cref="HeaderValue"/> to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> object is equal to the <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(object? left, HeaderValue right) => !right.Equals(left);

    /// <summary>
    /// Determines whether this instance and a specified object have the same value.
    /// </summary>
    /// <param name="obj">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to <paramref name="obj"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return Equals(this, HeaderValue.Empty);
        }

        if (obj is string str)
        {
            return Equals(this, str);
        }

        if (obj is string[] array)
        {
            return Equals(this, array);
        }

        if (obj is HeaderValue stringValues)
        {
            return Equals(this, stringValues);
        }

        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        object? value = this.values;
        if (value is string[] values)
        {
            if (Count == 1)
            {
                return Unsafe.As<string>(this[0])?.GetHashCode() ?? Count.GetHashCode();
            }
            int hashCode = 0;
            for (int i = 0; i < values.Length; i++)
            {
                // RyuJIT optimizes this to use the ROL instruction
                // Related GitHub pull request: https://github.com/dotnet/coreclr/pull/1830


                var rol5 = ((uint)hashCode << 5) | ((uint)hashCode >> 27);
                hashCode = ((int)rol5 + hashCode) ^ values[i]?.GetHashCode() ?? 0;
            }
            return hashCode;
        }
        else
        {
            return Unsafe.As<string>(value)?.GetHashCode() ?? Count.GetHashCode();
        }
    }

    /// <summary>
    /// Enumerates the string values of a <see cref="HeaderValue" />.
    /// </summary>
    public struct Enumerator : IEnumerator<string?>
    {
        private readonly string?[]? _values;
        private int _index;
        private string? _current;

        internal Enumerator(object? value)
        {
            if (value is string str)
            {
                _values = null;
                _current = str;
            }
            else
            {
                _current = null;
                _values = Unsafe.As<string?[]>(value);
            }
            _index = 0;
        }

        public Enumerator(ref HeaderValue values) : this(values.values)
        { }

        public bool MoveNext()
        {
            int index = _index;
            if (index < 0)
            {
                return false;
            }

            string?[]? values = _values;
            if (values != null)
            {
                if ((uint)index < (uint)values.Length)
                {
                    _index = index + 1;
                    _current = values[index];
                    return true;
                }

                _index = -1;
                return false;
            }

            _index = -1; // sentinel value
            return _current != null;
        }

        public string? Current => _current;

        object? IEnumerator.Current => _current;

        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }
    }
}


