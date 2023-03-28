using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal static class ExpressionUtilities
{
    // Will cache reflection request for methods already obtained
    private static ConcurrentDictionary<string, MethodInfo> methods;


    static ExpressionUtilities()
    {
        methods = new();
    }

    #region System.Reflection Method Info Utilities

    private static MethodInfo GetMathMethod(Type methodType, string methodName)
    {
        return methods.GetOrAdd(methodName, name =>
        {
            var method = typeof(Math).GetMethod(
                name,
                new Type[] 
                { 
                    methodType 
                });

            if (method is null)
            {
                throw new Exception();
            }

            return method;
        });
    }

    public static MethodInfo GetRoundMethod(Type type)
    {

        return GetMathMethod(type, nameof(Math.Round));
    }
    public static MethodInfo GetCeilingMethod(Type type)
    {
        if (type != typeof(double) || type != typeof(decimal))
        {
            throw new InvalidOperationException(
                "Invalid method call. Math.Floor() can only be called on the following types: 'double', and 'decimal'.");
        }
        return GetMathMethod(type, nameof(Math.Ceiling));
    }
    public static MethodInfo GetFloorMethod(Type type)
    {
        if (type != typeof(double) || type != typeof(decimal))
        {
            throw new InvalidOperationException(
                "Invalid method call. Math.Floor() can only be called on the following types: 'double', and 'decimal'.");
        }
        return GetMathMethod(type, nameof(Math.Floor));
    }
    public static MethodInfo GetAbsMethod(Type type)
    {
        var types = new Type[]
        {
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(nint),
            typeof(sbyte),
            typeof(short)
        };
        if (!types.Contains(type))
        {
            throw new InvalidOperationException(
                $"Invalid method call. Math.Abs() can only be called on the following types: {string.Join(',', types.Select(type => $" {type.Name}"))}");
        }

        return GetMathMethod(type, nameof(Math.Abs));
    }
    //public static MethodInfo GetAcosMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Acos, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Acos, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Acos, method);
    //    return method;
    //}
    //public static MethodInfo GetAsinMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Asin, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Asin, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Asin, method);
    //    return method;
    //}
    //public static MethodInfo GetAtanMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Atan, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Atan, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Atan, method);
    //    return method;
    //}

    //public static MethodInfo GetCosMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Cos, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Cos, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Cos, method);
    //    return method;
    //}
    //public static MethodInfo GetExpMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Exp, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Exp, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Exp, method);
    //    return method;
    //}
    //public static MethodInfo GetLogMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Log, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Log, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Log, method);
    //    return method;
    //}
    //public static MethodInfo GetLogTenMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Log10, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Log10, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Log10, method);
    //    return method;
    //}
    //public static MethodInfo GetPowMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Pow, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Pow, new Type[] { typeof(double), typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Pow, method);
    //    return method;
    //}
    //public static MethodInfo GetSingMethod(Type type)
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Sign, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Sign, new Type[] { type });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Sign, method);
    //    return method;
    //}

    //public static MethodInfo GetSinMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Sin, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Sin, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Sin, method);
    //    return method;
    //}

    //public static MethodInfo GetSqrtMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Sqrt, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Sqrt, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Sqrt, method);
    //    return method;
    //}

    //public static MethodInfo GetTanMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Tan, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Tan, new Type[] { typeof(double) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Tan, method);
    //    return method;
    //}

    //public static MethodInfo GetTruncateMethod(Type type)
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Truncate, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(Math).GetMethod(CosmosLinqToSqlFunctions.Truncate, new Type[] { type });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Truncate, method);
    //    return method;
    //}


    //public static MethodInfo GetStartsWithMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.StartsWith, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.StartsWith, new Type[] { typeof(string), typeof(StringComparison) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.StartsWith, method);
    //    return method;
    //}

    //public static MethodInfo GetEndsWithMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.EndsWith, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.EndsWith, new Type[] { typeof(string), typeof(StringComparison) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.EndsWith, method);
    //    return method;
    //}
    //public static MethodInfo GetToUpperMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.ToUpper, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.ToUpper, new Type[] { });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.ToUpper, method);
    //    return method;
    //}


    //public static MethodInfo GetToLowerMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.ToLower, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.ToLower, new Type[] { });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.ToLower, method);
    //    return method;
    //}

    //public static MethodInfo GetContainsMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Contains, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.Contains, new Type[] { typeof(string), typeof(StringComparison) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Contains, method);
    //    return method;
    //}

    //public static MethodInfo GetSubStringMethod()
    //{
    //    MethodInfo method = null;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.SubString, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.SubString, new Type[] { typeof(int), typeof(int) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.SubString, method);
    //    return method;
    //}

    //public static MethodInfo GetReplaceMethod()
    //{
    //    MethodInfo method = null;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Replace, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.Replace, new Type[] { typeof(string), typeof(string) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Replace, method);
    //    return method;
    //}
    //public static MethodInfo GetStringEqualsMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.StringEquals, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod("Equals", new Type[] { typeof(string), typeof(StringComparison) });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.StringEquals, method);
    //    return method;
    //}

    //public static MethodInfo GetTrimMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.Trim, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.Trim, new Type[] { });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.Trim, method);
    //    return method;

    //}

    //public static MethodInfo GetTrimStartMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.TrimStart, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.TrimStart, new Type[] { });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.TrimStart, method);
    //    return method;
    //}

    //public static MethodInfo GetTrimEndMethod()
    //{
    //    MethodInfo method;
    //    if (methods.TryGetValue(CosmosLinqToSqlFunctions.TrimEnd, out var cachedMethod))
    //        return cachedMethod;

    //    method = typeof(string).GetMethod(CosmosLinqToSqlFunctions.TrimEnd, new Type[] { });
    //    methods.TryAdd(CosmosLinqToSqlFunctions.TrimEnd, method);
    //    return method;
    //}
    #endregion


}
