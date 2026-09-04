namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtMedia
(
	SymbolToken atSign, AtKeywordToken media, IValue mediaQuery, Block block
) : SourceElement, ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Media => media;
	public IValue MediaQuery => mediaQuery;
	public Block Block => block;

	public SourceSpan Span => SourceSpan.From(atSign, block);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, mediaQuery, block);
}
