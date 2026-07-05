namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtCharset
(
	SymbolToken atSign, IdentifierToken charset, StringToken value, SymbolToken? semiColon
) : SourceElement, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public IdentifierToken Charset => charset;
	public StringToken Value => value;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, value, semiColon);

	public SourceCoordinates Start => Value.Start;

	public SourceCoordinates End => LastEnd(value, semiColon);
}
