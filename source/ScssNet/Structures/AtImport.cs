namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtImport
(
	SymbolToken atSign, AtKeywordToken import, IValue path, SymbolToken? semiColon
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Import => import;
	public IValue Path => path;
	public SymbolToken? SemiColon => semiColon;

	public SourceSpan Span => SourceSpan.From(atSign, path, semiColon);

	public Issues Issues => Issues.ConcatFrom(atSign, import, path, semiColon);
}
