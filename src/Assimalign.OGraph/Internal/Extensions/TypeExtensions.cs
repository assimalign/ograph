using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Assimalign.OGraph.Internal;

internal static class TypeExtensions
{
    /// <summary>
    /// Identifies whether type implements the IEnumerable interface
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsEnumerableType(this Type type, bool search = false)
    {
        if (type.IsGenericType)
        {
            if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return true;

            var interfaces = type.GetInterfaces();
            foreach (var argument in type.GetGenericArguments())
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(argument);

                if (interfaces.Contains(enumerableType))
                    return true;
            }
        }
        else if (search)
        {
            return type.FindInterfaces((filter, criteria) => IsEnumerableType(filter), null)[0] != null;
        }

        return false;
    }

    /// <summary>
    /// Identifies whether type implements the IEnumerable interface and returns interface implementation type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation">The IEnumerable Implementation</param>
    /// <returns></returns>
    public static bool IsEnumerableType(this Type type, out Type implementation)
    {
        implementation = null;
        if (type.IsGenericType)
        {
            var arguments = type.GetGenericArguments();
            if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                implementation = arguments[0];
                return true;
            }
            var interfaces = type.GetInterfaces();
            foreach (var argument in arguments)
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(argument);

                if (interfaces.Contains(enumerableType))
                {
                    implementation = argument;
                    return true;
                }
            }
        }
        else
        {
            var hasIntefaces = type.GetInterfaces().Length > 0;

            if (hasIntefaces)
            {
                var other = type.FindInterfaces((filter, criteria) => IsEnumerableType(filter), null).First();

                if (null != other)
                {
                    implementation = other.GetGenericArguments().First();
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public static bool ICollectionType(this Type type, bool checkNullable = true)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    public static bool ICollectionType(this Type type, out Type implementation, bool checkNullable = true)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation">Returns the type element of the array. For example if the type is 'int[]' then the return type is 'int'.</param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    /// <remarks>Regardless of an array being nullable the type is always evaluated as an array.</remarks>
    public static bool IsArrayType(this Type type, out Type implementation)
    {
        var isArray = false;
        if (type.IsArray)
        {
            implementation = type.GetElementType();
            isArray = true;
        }
        else
            implementation = null;
        return isArray;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public static bool IsArrayType(this Type type) =>
        type.IsArray;

    /// <summary>
    /// Checks if Type is Dictionary Type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="breakout">A flag to search for all interface and inherited implementations of parent 
    /// type. Will search for the base most interface IDictionary.
    /// </param>
    public static bool IsDictionaryType(this Type type, bool breakout = false)
    {
        var arguments = type.GenericTypeArguments;
        var dictionaryTypes = new Type[]
        {
                typeof(IDictionary<,>),
                typeof(Dictionary<,>),
                typeof(ConcurrentDictionary<,>)
        };

        if (arguments.Length == 2)
        {
            foreach (var dictionaryType in dictionaryTypes)
            {
                if (type == dictionaryType.MakeGenericType(arguments))
                {
                    return true;
                }
            }
        }

        if (breakout)
        {
            return type.GetInterfaces().Any(x => x == typeof(IDictionary));
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="keyImplementation">The type of the key being implemented in the dictionary.</param>
    /// <param name="valueImplementation">The type of the the value being implemented in the dictionary.</param>
    /// <param name="breakout">A flag to search for all interface and inherited implementations of parent 
    /// type. Will search for the base most interface IDictionary.
    /// </param>
    public static bool IsDictionaryType(this Type type, out Type keyImplementation, out Type valueImplementation, bool breakout = false)
    {
        keyImplementation = null;
        valueImplementation = null;

        var arguments = type.GenericTypeArguments;
        var dictionaryTypes = new Type[]
        {
                typeof(IDictionary<,>),
                typeof(Dictionary<,>),
                typeof(ConcurrentDictionary<,>)
        };

        if (arguments.Length == 2)
        {
            foreach (var dictionaryType in dictionaryTypes)
            {
                if (type == dictionaryType.MakeGenericType(arguments))
                {
                    keyImplementation = arguments[0];
                    valueImplementation = arguments[1];
                    return true;
                }
            }
        }
        if (breakout)
        {
            return type.GetInterfaces().Any(x => x == typeof(IDictionary));
        }

        return false;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public static bool IsComplexType(this Type type)
    {
        if (type.IsClass &&
            !type.IsArray &&
            !type.IsEnumerableType() &&
            type != typeof(string))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Checks if type consist of all numeric types: signed integers, unsigned integers, and floating point.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsNumericType(this Type type, bool checkNullable = true)
    {
        if (type.IsSignedNumericType(checkNullable))
        {
            return true;
        }
        if (type.IsUnsignedNumericType(checkNullable))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if type consist of all numeric types: signed integers, unsigned 
    /// integers, and floating point then returns the implementation.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsNumericType(this Type type, out Type implementation, bool checkNullable = true)
    {
        implementation = null;

        if (type.IsSignedNumericType(out var signedNumericType, checkNullable))
        {
            implementation = signedNumericType;
            return true;
        }
        if (type.IsUnsignedNumericType(out var unsignedNumericType, checkNullable))
        {
            implementation = unsignedNumericType;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsInteger16Type(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(short) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(short));
        }
        else if (type == typeof(short))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsInteger32Type(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(int) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(int));
        }
        else if (type == typeof(int))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsInteger64Type(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(long) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(long));
        }
        else if (type == typeof(long))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsDoubleType(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(double) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(double));
        }
        else if (type == typeof(double))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsDecimalType(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(decimal) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(decimal));
        }
        else if (type == typeof(decimal))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Checks if the type is a floating point numeric type with a precision of 6-9 digits.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsFloatType(this Type type, bool checkNullable = true)
    {
        if (checkNullable)
        {
            return type == typeof(float) ||
                type == typeof(Nullable<>).MakeGenericType(typeof(float));
        }
        else if (type == typeof(float))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsFloatingPointNumericType(this Type type, out Type implementation, bool checkNullable = true)
    {
        implementation = null;
        var numericTypes = new Type[]
        {
                typeof(double),
                typeof(decimal),
                typeof(float)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                implementation = numericType;
                return true;
            }
            else if (type == numericType)
            {
                implementation = numericType;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsFloatingPointNumericType(this Type type, bool checkNullable = true)
    {
        var numericTypes = new Type[]
        {
                typeof(double),
                typeof(decimal),
                typeof(float)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                return true;
            }
            else if (type == numericType)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <returns></returns>
    public static bool IsSignedNumericType(this Type type, bool checkNullable = true)
    {
        var numericTypes = new Type[]
        {
                typeof(sbyte),
                typeof(short), 	// Int16
				typeof(int),	// Int32
				typeof(long),	// Int64
				typeof(double),
                typeof(decimal),
                typeof(float),	// Single
				typeof(nint)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                return true;
            }
            else if (type == numericType)
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Checks whether the type is Signed Numeric (meaning it can be negative or positive)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation"></param>
    /// <param name="checkNullable"></param>
    public static bool IsSignedNumericType(this Type type, out Type implementation, bool checkNullable = true)
    {
        implementation = null;
        var numericTypes = new Type[]
        {
                typeof(sbyte),
                typeof(short), 	// Int16
				typeof(int),	// Int32
				typeof(long),	// Int64
				typeof(double),
                typeof(decimal),
                typeof(float),	// Single
				typeof(nint)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                implementation = numericType;
                return true;
            }
            else if (type == numericType)
            {
                implementation = numericType;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Check wether the numeric type is a and unsigned integer.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    /// <remarks>Unsigned numeric types are values which are above zero.</remarks>
    /// <returns>bool</returns>
    public static bool IsUnsignedNumericType(this Type type, bool checkNullable = true)
    {
        var numericTypes = new Type[]
        {
                typeof(byte),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(nuint)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                return true;
            }
            else if (type == numericType)
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Check wether the numeric type is a and unsigned integer and outputs the numeric type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation"></param>
    /// <param name="checkNullable"></param>
    public static bool IsUnsignedNumericType(this Type type, out Type implementation, bool checkNullable = true)
    {
        implementation = null;
        var numericTypes = new Type[]
        {
                typeof(byte),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(nuint)
        };

        foreach (var numericType in numericTypes)
        {
            if (checkNullable && type == typeof(Nullable<>).MakeGenericType(numericType))
            {
                implementation = numericType;
                return true;
            }
            else if (type == numericType)
            {
                implementation = numericType;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    public static bool IsBooleanType(this Type type, bool checkNullable = true)
    {
        if (checkNullable && type == typeof(Nullable<>).MakeGenericType(typeof(bool)))
        {
            return true;
        }
        if (type == typeof(bool))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    public static bool IsGuidType(this Type type, bool checkNullable = true)
    {
        if (checkNullable && type == typeof(Nullable<>).MakeGenericType(typeof(Guid)))
        {
            return true;
        }
        if (type == typeof(Guid))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public static bool IsNullable(this Type type)
    {
        var arguments = type.GenericTypeArguments;

        if (arguments.Any() && arguments.Length == 1)
        {
            if (type == typeof(Nullable<>).MakeGenericType(arguments[0]))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public static bool IsNullable(this Type type, out Type implementation)
    {
        implementation = null;
        var arguments = type.GenericTypeArguments;

        if (arguments.Any() && arguments.Length == 1)
        {
            if (type == typeof(Nullable<>).MakeGenericType(arguments[0]))
            {
                implementation = arguments[0];
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    public static bool IsDateTimeType(this Type type, bool checkNullable = true)
    {
        if (checkNullable && type == typeof(Nullable<>).MakeGenericType(typeof(DateTime)))
        {
            return true;
        }
        if (type == typeof(DateTime))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    public static bool IsTimeSpanType(this Type type, bool checkNullable = true)
    {
        if (checkNullable && type == typeof(Nullable<>).MakeGenericType(typeof(TimeSpan)))
        {
            return true;
        }
        if (type == typeof(TimeSpan))
        {
            return true;
        }

        return false;
    }


    public static bool IsStringType(this Type type)
    {
        return type == typeof(string);
    }
    /// <summary>
    /// Certain types in the .NET such as strings aren't considered value types 
    /// since they are mutable. Need to considered types such as these to be value types
    /// as if will cut down on the type checking for the expression building.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="checkNullable"></param>
    public static bool IsValueType(this Type type, bool checkNullable = true)
    {
        var isValueType = type.IsValueType;

        // Will use this array of types to check for nullable value and enum types
        var valueTypes = new Type[]
        {
                typeof(Boolean),
                typeof(Byte),
                typeof(Char),
                typeof(DateOnly),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(Decimal),
                typeof(Double),
                typeof(Guid),
                typeof(Half),
                typeof(Index),
#if NET7_0_OR_GREATER
		        typeof(Int128),
                typeof(UInt128),
#endif
                typeof(Int16),
                typeof(Int32),
			    typeof(Int64),
                typeof(IntPtr),
                typeof(Range),
                typeof(Single),
                typeof(UInt16),
                typeof(UInt32),
                typeof(UInt64),
                typeof(SByte),
                typeof(TimeSpan),
                typeof(TimeOnly),
                typeof(UIntPtr)
        };

        // Let's ensure that the type is not wrapped in the Nullable<> type class
        if (checkNullable)
        {
            foreach (var valueType in valueTypes)
            {
                if (type == typeof(Nullable<>).MakeGenericType(valueType))
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (var valueType in valueTypes)
            {
                if (type == valueType)
                {
                    return true;
                }
            }
        }

        return isValueType;
    }


    /// <summary>
    /// Certain types in the .NET such as strings aren't considered value types 
    /// since they are mutable. Need to considered types such as these to be value types
    /// as if will cut down on the type checking for the expression building.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="implementation"></param>
    public static bool IsValueType(this Type type, out Type implementation, bool checkNullable = true)
    {
        implementation = null;

        var isValueType = type.IsValueType;

        // Will use this array of types to check for nullable value and enum types
        var valueTypes = new Type[]
        {
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(double),
                typeof(decimal),
                typeof(float),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(char),
                typeof(byte),
                typeof(sbyte),
                typeof(bool),
                typeof(Guid),
                typeof(DateTime),
                typeof(TimeSpan),
                typeof(nint),
                typeof(nuint),
                typeof(string)
        };

        if (!isValueType)
        {
            return false;
        }


        // Let's ensure that the type is not wrapped in the Nullable<> type class
        if (checkNullable)
        {
            foreach (var valueType in valueTypes)
            {
                if (type == typeof(Nullable<>).MakeGenericType(valueType))
                {
                    implementation = valueType;
                    return true;
                }
            }
        }
        else
        {
            foreach (var valueType in valueTypes)
            {
                if (type == valueType)
                {
                    implementation = valueType;
                    return true;
                }
            }
        }

        return false;
    }




    /// <summary>
    /// Creates a basic Deep Clone of an object. 
    /// </summary>
    /// <param name="source"></param>
    public static object Clone(this object source)
    {
        // Get the type of source object and create a new instance of that type
        var sourceType = source.GetType();
        var instance = Activator.CreateInstance(sourceType);

        // Get all the properties of source object type that are readable and writable
        var properties = sourceType
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(prop => prop.CanRead && prop.CanWrite);

        // Assign all source property to taget object 's properties
        foreach (var property in properties)
        {
            // Check whether property type is value type, enum or string type
            if (property.PropertyType.IsValueType())
            {
                property.SetValue(instance, property.GetValue(source, null), null);
            }
            // Assumption: property type is object/complex types, so need to recursively call this method until the end of the tree is reached
            else
            {
                var value = property.GetValue(source, null);
                if (value == null)
                {
                    property.SetValue(instance, null, null);
                }
                else
                {
                    property.SetValue(instance, value.Clone(), null);
                }
            }
        }

        return instance;
    }
}
