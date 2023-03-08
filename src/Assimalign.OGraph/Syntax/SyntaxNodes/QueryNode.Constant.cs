using System;
using System.Linq;
using System.Text;

namespace Assimalign.OGraph.Syntax;

public sealed class ConstantQueryNode : QueryNode
{
    private byte[] value = new byte[0];

    internal ConstantQueryNode() { }
    public ConstantQueryNode(byte[] value)
    {
        this.Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte[] Value
    {
        get => this.value;
        init
        {
            this.value = value;
        }
    }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Constant;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    #region Parse
    public string GetString() => Encoding.UTF8.GetString(Value);
    public DateOnly GetDate() => DateOnly.Parse(GetString());
    public bool TryGetDate(out DateOnly date) => DateOnly.TryParse(GetString(), out date);
    public DateTime GetDateTime() => DateTime.Parse(GetString());
    public bool TryGetDateTime(out DateTime dateTime) => DateTime.TryParse(GetString(), out dateTime);
    public TimeOnly GetTime() => TimeOnly.Parse(GetString());
    public bool TryGetTime(out TimeOnly time) => TimeOnly.TryParse(GetString(), out time);
    public short GetInt16() => short.Parse(GetString());
    public bool TryGetInt16(out short int16) => short.TryParse(GetString(), out int16);
    public int GetInt32() => int.Parse(GetString());
    public bool TryGetInt32(out int int32) => int.TryParse(GetString(), out int32);
    public long GetInt64() => long.Parse(GetString());
    public bool TryGetInt64(out long int64) => long.TryParse(GetString(), out int64);
    public decimal GetDecimal() => decimal.Parse(GetString());
    public bool TryGetDecimal(out decimal deci) => decimal.TryParse(GetString(), out deci);
    public float GetSingle() => float.Parse(GetString());
    public bool TryGetSingle(out float single) => float.TryParse(GetString(), out single);
    #endregion
}
