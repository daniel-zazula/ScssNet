using ScssNet.Tokens;

namespace ScssNet.Structures;

public class RuleSet(SelectorList selectorlist, SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace)
	: SourceElement, ISyntaxStructure, INestableStatement
{
	public SelectorList SelectorList => selectorlist;
	public SymbolToken OpenBrace => openBrace;
	public ICollection<Rule> Rules => rules;
	public SymbolToken CloseBrace => closeBrace;

	public IEnumerable<Issue> Issues => ConcatIssues();

	public SourceCoordinates Start => SelectorList.Start;

	public SourceCoordinates End => CloseBrace.End;

	private IEnumerable<Issue> ConcatIssues()
	{
		var elements = new ISourceElement[] { SelectorList, OpenBrace }
			.Concat(Rules)
			.Append(CloseBrace);

		return ConcatIssuesFrom(elements);
	}
}
