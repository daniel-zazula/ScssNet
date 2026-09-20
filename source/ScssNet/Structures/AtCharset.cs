namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtCharset
(
	SymbolToken atSign, AtKeywordToken charset, StringToken charsetName, SymbolToken? semiColon
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Charset => charset;
	public StringToken CharsetName => charsetName;
	public SymbolToken? SemiColon => semiColon;

	public SourceSpan Span => SourceSpan.From(atSign, charsetName, semiColon);

	public Issues Issues => Issues.ConcatFrom(atSign, charsetName, semiColon);
}
