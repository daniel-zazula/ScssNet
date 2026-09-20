namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtMedia
(
	SymbolToken atSign, AtKeywordToken media, IMediaQuery mediaQuery, Block block
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Media => media;
	public IMediaQuery MediaQuery => mediaQuery;
	public Block Block => block;

	public SourceSpan Span => SourceSpan.From(atSign, block);

	public Issues Issues => Issues.ConcatFrom(atSign, mediaQuery, block);
}
