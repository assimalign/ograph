using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public record Either<T1, T2> : IEither
{
    public static implicit operator Either<T1, T2>(T1 value) => 
        new Either<T1, T2>(value);

    public static implicit operator Either<T1, T2>(T2 value) => 
        new Either<T1, T2>(value);

    public static implicit operator Either<T1, T2>(Either<T2, T1> other) =>
        new Either<T1, T2>(other.valueType == 1 ? 2 : 1, other.value);

    Either(int populatedType, object value) => (valueType, value) = (populatedType, value);

    public Either(T1 value) 
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        this.value = value; 
        this.valueType = 1; 
    }
    public Either(T2 value) 
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        this.value = value; 
        this.valueType = 2; 
    }

    T1 AsT1 => (T1)value;
    T2 AsT2 => (T2)value;

    int IEither.ValueType => valueType;
    object IEither.Value => value;

    int valueType;
    object value;

    
    public void Switch(Action<T1> ifT1, Action<T2> ifT2)
    {
        if (valueType == 1)
            ifT1(AsT1);
        else
            ifT2(AsT2);
    }
    public Either<T1, T2> Match<TResult1>(Func<T1, T2> ifT1, Func<T1, bool> when)
    {
        if (valueType == 1)
        {
            if (when(AsT1))
                return ifT1(AsT1);
            else
                return AsT1;
        }
        else
            return AsT2;
    }
    public Either<TResult1, T2> Match<TResult1>(Func<T1, TResult1> ifT1)
    {
        if (valueType == 1)
            return ifT1(AsT1);
        else
            return AsT2;
    }
    public Either<T1, T2, TResult1> Match<TResult1>(Func<T1, TResult1> ifT1, Func<T1, bool> when)
    {
        if (valueType == 1)
        {
            if (when(AsT1))
                return ifT1(AsT1);
            else
                return AsT1;
        }
        else
            return AsT2;
    }
   
    public Either<T1, T2> Match<TResult2>(Func<T2, T1> ifT2, Func<T2, bool> when)
    {
        if (valueType == 2)
        {
            if (when(AsT2))
                return ifT2(AsT2);
            else
                return AsT2;
        }
        else
            return AsT1;
    }
    public Either<TResult2, T1> Match<TResult2>(Func<T2, TResult2> ifT2)
    {
        if (valueType == 2)
            return ifT2(AsT2);
        else
            return AsT1;
    }
    public Either<T1, T2, TResult2> Match<TResult2>(Func<T2, TResult2> ifT2, Func<T2, bool> when)
    {
        if (valueType == 2)
        {
            if (when(AsT2))
                return ifT2(AsT2);
            else
                return AsT2;
        }
        else
            return AsT1;
    }

    public T2 Match(Func<T1, T2> ifT1)
    {
        if (valueType == 1)
            return ifT1(AsT1);
        else
            return AsT2;
    }
    public T1 Match(Func<T2, T1> ifT2)
    {
        if (valueType == 2)
            return ifT2(AsT2);
        else
            return AsT1;
    }

    public bool If(out T1 @if) => If(out @if, out _);
    public bool If(out T1 @if, out T2 @else)
    {
        if (valueType == 1)
        {
            @if = AsT1;
            @else = default;
            return true;
        }

        @if = default;
        @else = AsT2;
        return false;
    }
    public bool If(out T2 @if) => If(out @if, out _);
    public bool If(out T2 @if, out T1 @else)
    {
        if (valueType == 2)
        {
            @if = AsT2;
            @else = default;
            return true;
        }

        @if = default;
        @else = AsT1;
        return false;
    }

    public override string ToString() => valueType switch
    {
        1 => typeof(T1).Name + ":" + AsT1,
        2 => typeof(T2).Name + ":" + AsT2,
        _ => throw new InvalidOperationException()
    };
}
