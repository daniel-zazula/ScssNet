using ScssNet.Tokens;

namespace ScssNet.SourceElements
{
	public class Block(SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace) : ISourceElement
	{
		public SymbolToken OpenBrace => openBrace;
		public ICollection<Rule> Rules => rules;
		public SymbolToken CloseBrace => closeBrace;

		public IEnumerable<Issue> Issues => SourceElement.List(openBrace).Concat(rules).Append(closeBrace).ConcatIssues();

		public SourceCoordinates Start => openBrace.Start;

		public SourceCoordinates End => closeBrace.End;
	}
}
