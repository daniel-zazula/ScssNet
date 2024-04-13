using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Block(SymbolToken openBrace, ICollection<Rule> rules, SymbolToken closeBrace)
	{
		public SymbolToken OpenBrace => openBrace;
		public ICollection<Rule> Rules => rules;
		public SymbolToken CloseBrace => closeBrace;
	}

	internal class BlockParser(Lazy<RuleParser> ruleParser): ParserBase
	{
		internal Block? Parse(TokenReader tokenReader)
		{
			if(!Match(tokenReader, Symbol.OpenBrace, out var openBrace))
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
