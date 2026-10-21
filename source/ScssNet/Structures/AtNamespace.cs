namespace ScssNet.Structures;

using ScssNet.Tokens;

public class AtNamespace
(
	SymbolToken atSign, AtKeywordToken @namespace, IdentifierToken? prefix, IUrl url, SymbolToken? semiColon
) : ISyntaxStructure, IStatement, IAtRule
{
	public SymbolToken AtSign => atSign;
	public AtKeywordToken Namespace => @namespace;
	public IdentifierToken? Prefix => prefix;
	public IUrl Url => url;
	public SymbolToken? SemiColon => semiColon;

	public SourceSpan Span => SourceSpan.From(atSign, url, semiColon);

	public Issues Issues => Issues.ConcatFrom(atSign, @namespace, prefix, url, semiColon);
}
