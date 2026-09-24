namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtImport
(
	SymbolToken atSign, AtKeywordToken import, IValue url, IMediaQuery? mediaQuery, SymbolToken? semiColon
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Import => import;
	public IValue Url => url;
	public IMediaQuery? MediaQuery => mediaQuery;
	public SymbolToken? SemiColon => semiColon;

	public SourceSpan Span => SourceSpan.From(atSign, url, mediaQuery, semiColon);

	public Issues Issues => Issues.ConcatFrom(atSign, import, url, mediaQuery, semiColon);
}
