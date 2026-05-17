using ScssNet.Tokens;

namespace ScssNet.Structures;

public class Block(SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace) : SourceElement, ISyntaxStructure
{
	public SymbolToken OpenBrace => openBrace;
	public ICollection<Rule> Rules => rules;
	public SymbolToken CloseBrace => closeBrace;

	public IEnumerable<Issue> Issues => ListIssues();

	public SourceCoordinates Start => openBrace.Start;

	public SourceCoordinates End => closeBrace.End;

	private IEnumerable<Issue> ListIssues()
	{
		var elements = new ISourceElement[] { OpenBrace }
			.Concat(Rules)
			.Append(CloseBrace);

		return ConcatIssuesFrom(elements);
	}
}
