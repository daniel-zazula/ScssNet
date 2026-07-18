namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtCharset
(
	SymbolToken atSign, KeywordToken charset, StringToken charsetName, SymbolToken? semiColon
) : SourceElement, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public KeywordToken Charset => charset;
	public StringToken CharsetName => charsetName;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, charsetName, semiColon);

	public SourceCoordinates Start => CharsetName.Start;

	public SourceCoordinates End => LastEnd(charsetName, semiColon);
}
