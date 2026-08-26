namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtImport
(
	SymbolToken atSign, AtKeywordToken import, IValue path, SymbolToken? semiColon
) : SourceElement, ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Import => import;
	public IValue Path => path;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, path, semiColon);

	public SourceCoordinates Start => Path.Start;

	public SourceCoordinates End => LastEnd(path, semiColon);
}
