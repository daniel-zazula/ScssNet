using ScssNet.Tokens;

namespace ScssNet.Structures;

public class Block(SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace)
	: ISyntaxStructure
{
	public SymbolToken OpenBrace => openBrace;
	public ICollection<Rule> Rules => rules;
	public SymbolToken CloseBrace => closeBrace;

	public SourceSpan Span => SourceSpan.From(openBrace, closeBrace);

	public Issues Issues => ListIssues();

	private Issues ListIssues()
	{
		var elements = new ISourceElement[] { OpenBrace }
			.Concat(Rules)
			.Append(CloseBrace);

		return Issues.ConcatFrom(elements);
	}
}
