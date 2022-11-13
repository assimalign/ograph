using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public ref partial struct TokenLexer
{
    #region Other
    internal static ReadOnlySpan<byte> Tab => new byte[] { (byte)'\t' };
    internal static ReadOnlySpan<byte> CarriageReturn => new byte[] { (byte)'\r' };
    internal static ReadOnlySpan<byte> NewLine => new byte[] { (byte)'\n' };
    internal static ReadOnlySpan<byte> Dot => new byte[] { (byte)'.' };
    internal static ReadOnlySpan<byte> Comma => new byte[] { (byte)',' };
    internal static ReadOnlySpan<byte> Colon => new byte[] { (byte)':' };
    internal static ReadOnlySpan<byte> Semicolon => new byte[] { (byte)';' };
    internal static ReadOnlySpan<byte> WhiteSpace => new byte[] { (byte)' ' };
    internal static ReadOnlySpan<byte> SingleQuoteOperator => new byte[] { (byte)'\'' };
    internal static ReadOnlySpan<byte> DoubleQuote => new byte[] { (byte)'"' };
    internal static ReadOnlySpan<byte> OpenParentheses => new byte[] { (byte)'(' };
    internal static ReadOnlySpan<byte> CloseParentheses => new byte[] { (byte)')' };
    internal static ReadOnlySpan<byte> OpenBracket => new byte[] { (byte)'{' };
    internal static ReadOnlySpan<byte> CloseBracket => new byte[] { (byte)'}' };
    #endregion

    #region Operators	
    internal static ReadOnlySpan<byte> StarOperator => new byte[] { (byte)'*' };
    internal static ReadOnlySpan<byte> PlusOperator => new byte[] { (byte)'+' };
    internal static ReadOnlySpan<byte> MinusOperator => new byte[] { (byte)'-' };
    internal static ReadOnlySpan<byte> EqualOperator => new byte[] { (byte)'e', (byte)'q' };
    internal static ReadOnlySpan<byte> NotEqualOperator => new byte[] { (byte)'n', (byte)'e', (byte)'q' };
    internal static ReadOnlySpan<byte> GreaterThanOperator => new byte[] { (byte)'g', (byte)'t' };
    internal static ReadOnlySpan<byte> GreaterThanOrEqualOperator => new byte[] { (byte)'g', (byte)'t', (byte)'e' };
    internal static ReadOnlySpan<byte> LessThanOperator => new byte[] { (byte)'l', (byte)'t' };
    internal static ReadOnlySpan<byte> LessThanOrEqualOperator => new byte[] { (byte)'l', (byte)'t', (byte)'e' };
    internal static ReadOnlySpan<byte> AndOperator => new byte[] { (byte)'a', (byte)'n', (byte)'d' };
    internal static ReadOnlySpan<byte> OrOperator => new byte[] { (byte)'o', (byte)'r' };
    internal static ReadOnlySpan<byte> AliasOperator => new byte[] { (byte)'a', (byte)'s' };
    #endregion

    #region Literals
    internal static ReadOnlySpan<byte> BooleanTrueLiteral => Encoding.UTF8.GetBytes("true");
    internal static ReadOnlySpan<byte> BooleanFalseLiteral => Encoding.UTF8.GetBytes("false");
    #endregion

    #region Keywords
    internal static ReadOnlySpan<byte> SelectClause => Encoding.UTF8.GetBytes("select");
    internal static ReadOnlySpan<byte> FilterClause => Encoding.UTF8.GetBytes("filter");
    internal static ReadOnlySpan<byte> PageClause => Encoding.UTF8.GetBytes("page");
    internal static ReadOnlySpan<byte> SortClause => Encoding.UTF8.GetBytes("sort");
    internal static ReadOnlySpan<byte> AsClause => new byte[] { (byte)'a', (byte)'s' };
    #endregion
}
