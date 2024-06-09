using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Block(SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace): ISourceElement
	{
		public SymbolToken OpenBrace => openBrace;
		public ICollection<Rule> Rules => rules;
		public SymbolToken CloseBrace => closeBrace;

		public IEnumerable<Issue> Issues => SourceElement.List(openBrace).Concat(rules).Append(closeBrace).ConcatIssues();

		public SourceCoordinates Start => openBrace.Start;

		public SourceCoordinates End => closeBrace.End;
	}

	internal class BlockParser(Lazy<RuleParser> ruleParser): ParserBase
	{
		internal Block? Parse(TokenReader tokenReader)
		{
			var openBrace = Match(tokenReader, Symbol.OpenBrace);
			if(openBrace is null)
				return null;

			var rule = ruleParser.Value.Parse(tokenReader);
			var rules = new List<Rule>();
			while(rule != null)
			{
				rules.Add(rule);
				rule = ruleParser.Value.Parse(tokenReader);
			}

			var closeBrace = Require(tokenReader, Symbol.CloseBrace);
			return new Block(openBrace!, rules, closeBrace);
		}
	}
}
