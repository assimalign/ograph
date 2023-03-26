using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public record Either<T1, T2, T3, T4> : IEither
{
    public static implicit operator Either<T1, T2, T3, T4>(T1 x) => new Either<T1, T2, T3, T4>(x);
    public static implicit operator Either<T1, T2, T3, T4>(T2 x) => new Either<T1, T2, T3, T4>(x);
    public static implicit operator Either<T1, T2, T3, T4>(T3 x) => new Either<T1, T2, T3, T4>(x);
    public static implicit operator Either<T1, T2, T3, T4>(T4 x) => new Either<T1, T2, T3, T4>(x);

    int valueType;
    object value;

    int IEither.ValueType => valueType;
    object IEither.Value => value;

    T1 AsT1 => (T1)value;
    T2 AsT2 => (T2)value;
    T3 AsT3 => (T3)value;
    T4 AsT4 => (T4)value;

    public Either(T1 value) { this.value = value; valueType = 1; }
    public Either(T2 value) { this.value = value; valueType = 2; }
    public Either(T3 value) { this.value = value; valueType = 3; }
    public Either(T4 value) { this.value = value; valueType = 4; }

    Either() { }

    public void Switch(Action<T1> ifT1, Action<T2> ifT2, Action<T3> ifT3, Action<T4> ifT4)
    {
        switch (valueType)
        {
            case 1: ifT1(AsT1); break;
            case 2: ifT2(AsT2); break;
            case 3: ifT3(AsT3); break;
            case 4: ifT4(AsT4); break;
        }
    }

    public Either<TResult1, T2, T3, T4> Match<TResult1>(Func<T1, TResult1> ifT1) => valueType switch
    {
        1 => ifT1(AsT1),
        2 => AsT2,
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T2, T3, T4> Match(Func<T1, T2> ifT1) => valueType switch
    {
        1 => ifT1(AsT1),
        2 => AsT2,
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T2, T3, T4> Match(Func<T1, T3> ifT1) => valueType switch
    {
        1 => ifT1(AsT1),
        2 => AsT2,
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T2, T3, T4> Match(Func<T1, T4> ifT1) => valueType switch
    {
        1 => ifT1(AsT1),
        2 => AsT2,
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };

    public Either<T1, TResult2, T3, T4> Match<TResult2>(Func<T2, TResult2> ifT2) => valueType switch
    {
        1 => AsT1,
        2 => ifT2(AsT2),
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T3, T4> Match(Func<T2, T1> ifT2) => valueType switch
    {
        1 => AsT1,
        2 => ifT2(AsT2),
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T3, T4> Match(Func<T2, T3> ifT2) => valueType switch
    {
        1 => AsT1,
        2 => ifT2(AsT2),
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T3, T4> Match(Func<T2, T4> ifT2) => valueType switch
    {
        1 => AsT1,
        2 => ifT2(AsT2),
        3 => AsT3,
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };

    public Either<T1, T2, TResult3, T4> Match<TResult3>(Func<T3, TResult3> ifT3) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => ifT3(AsT3),
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T4> Match(Func<T3, T1> ifT3) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => ifT3(AsT3),
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T4> Match(Func<T3, T2> ifT3) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => ifT3(AsT3),
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T4> Match(Func<T3, T4> ifT3) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => ifT3(AsT3),
        4 => AsT4,
        _ => throw new InvalidOperationException()
    };

    public Either<T1, T2, T3, TResult4> Match<TResult4>(Func<T4, TResult4> ifT4) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => AsT3,
        4 => ifT4(AsT4),
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T3> Match(Func<T4, T1> ifT4) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => AsT3,
        4 => ifT4(AsT4),
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T3> Match(Func<T4, T2> ifT4) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => AsT3,
        4 => ifT4(AsT4),
        _ => throw new InvalidOperationException()
    };
    public Either<T1, T2, T3> Match(Func<T4, T3> ifT4) => valueType switch
    {
        1 => AsT1,
        2 => AsT2,
        3 => AsT3,
        4 => ifT4(AsT4),
        _ => throw new InvalidOperationException()
    };

    public bool If(out T1 @if) => If(out @if, out _);
    public bool If(out T1 @if, out Either<T2, T3, T4> @else)
    {
        switch (valueType)
        {
            case 1:
                @if = AsT1;
                @else = default;
                return true;
            case 2:
                @if = default;
                @else = AsT2;
                return false;
            case 3:
                @if = default;
                @else = AsT3;
                return false;
            default:
                @if = default;
                @else = AsT4;
                return false;
        }
    }

    public bool If(out T2 @if) => If(out @if, out _);
    public bool If(out T2 @if, out Either<T1, T3, T4> @else)
    {
        switch (valueType)
        {
            case 1:
                @if = default;
                @else = AsT1;
                return false;
            case 2:
                @if = AsT2;
                @else = default;
                return true;
            case 3:
                @if = default;
                @else = AsT3;
                return false;
            default:
                @if = default;
                @else = AsT4;
                return false;
        }
    }

    public bool If(out T3 @if) => If(out @if, out _);
    public bool If(out T3 @if, out Either<T1, T2, T4> @else)
    {
        switch (valueType)
        {
            case 1:
                @if = default;
                @else = AsT1;
                return false; ;
            case 2:
                @if = default;
                @else = AsT2;
                return false;
            case 3:
                @if = AsT3;
                @else = default;
                return true;
            default:
                @if = default;
                @else = AsT4;
                return false;
        }
    }

    public bool If(out T4 @if) => If(out @if, out _);
    public bool If(out T4 @if, out Either<T1, T2, T3> @else)
    {
        switch (valueType)
        {
            case 1:
                @if = default;
                @else = AsT1;
                return false;
            case 2:
                @if = default;
                @else = AsT2;
                return false;
            case 3:
                @if = default;
                @else = AsT3;
                return false;
            default:
                @if = AsT4;
                @else = default;
                return true;
        }
    }

    public override string ToString() => valueType switch
    {
        1 => typeof(T1).Name + ":" + AsT1,
        2 => typeof(T2).Name + ":" + AsT2,
        3 => typeof(T3).Name + ":" + AsT3,
        _ => throw new InvalidOperationException()
    };
}
