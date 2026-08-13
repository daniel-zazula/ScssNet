namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtMedia
(
	SymbolToken atSign, KeywordToken media, IValue mediaQuery, Block block
) : SourceElement, ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public KeywordToken Media => media;
	public IValue MediaQuery => mediaQuery;
	public Block Block => block;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(atSign, mediaQuery, block);

	public SourceCoordinates Start => MediaQuery.Start;

	public SourceCoordinates End => Block.End;
}
