namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtImport
(
	SymbolToken atSign, IdentifierToken import, StringToken path, SymbolToken? semiColon
) : SourceElement, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public IdentifierToken Import => import;
	public StringToken Path => path;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, path, semiColon);

	public SourceCoordinates Start => Path.Start;

	public SourceCoordinates End => LastEnd(path, semiColon);
}
