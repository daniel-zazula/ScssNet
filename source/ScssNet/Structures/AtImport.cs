namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtImport
(
	SymbolToken atSign, AtKeywordToken import, IValue path, IMediaQuery? mediaQuery, SymbolToken? semiColon
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Import => import;
	public IValue Path => path;
	public IMediaQuery? MediaQuery => mediaQuery;
	public SymbolToken? SemiColon => semiColon;

	public SourceSpan Span => SourceSpan.From(atSign, path, mediaQuery, semiColon);

	public Issues Issues => Issues.ConcatFrom(atSign, import, path, mediaQuery, semiColon);
}
