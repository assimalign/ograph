using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Syntax;


[DebuggerDisplay("{Text}")]
public sealed class ConstantNode : QueryNode
{
    internal ConstantNode(byte[] value, string text, Location location) 
        : base(text, location)
    {
        Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte[] Value { get; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Constant;

    /// <inheritdoc />
    public override void Accept(IQueryNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node)
        {
            yield return node;
        }
    }

    #region Parse
    public string GetString() => Encoding.UTF8.GetString(Value);
    public DateTime GetDateTime() => DateTime.Parse(GetString());
    public DateOnly GetDate() => DateOnly.Parse(GetString());
    public TimeOnly GetTime() => TimeOnly.Parse(GetString());
    public short GetInt16() => short.Parse(GetString());
    public int GetInt32() => int.Parse(GetString());
    public long GetInt64() => long.Parse(GetString());
    public float GetSingle() => float.Parse(GetString());
    public decimal GetDecimal() => decimal.Parse(GetString());

    public bool TryGetInt16(out short int16) => short.TryParse(GetString(), out int16);
    public bool TryGetInt64(out long int64) => long.TryParse(GetString(), out int64);
    public bool TryGetDateTime(out DateTime dateTime) => DateTime.TryParse(GetString(), out dateTime);
    public bool TryGetTime(out TimeOnly time) => TimeOnly.TryParse(GetString(), out time);
    public bool TryGetDate(out DateOnly date) => DateOnly.TryParse(GetString(), out date);
    public bool TryGetDecimal(out decimal deci) => decimal.TryParse(GetString(), out deci);
    public bool TryGetInt32(out int int32) => int.TryParse(GetString(), out int32);
    public bool TryGetSingle(out float single) => float.TryParse(GetString(), out single);
    #endregion
}
