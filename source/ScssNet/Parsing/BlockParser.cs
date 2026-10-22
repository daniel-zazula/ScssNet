using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IBlockParser : IParser<Block>, IRequiredParser<Block> { }

internal class BlockParser(ParserContext parserContext): IBlockParser
{
	public Block? Parse(TokenReader tokenReader)
	{
		var openBrace = tokenReader.MatchSymbol(Symbol.OpenBrace);
		if(openBrace is null)
			return null;

		return ParseRemainder(tokenReader, openBrace);
	}

	public Block Require(TokenReader tokenReader)
	{
		return ParseRemainder(tokenReader, tokenReader.RequireSymbol(Symbol.OpenBrace));
	}

	private Block ParseRemainder(TokenReader tokenReader, SymbolToken openBrace)
	{
		var ruleParser = parserContext.GetParser<IRuleParser>();

		var rules = new List<Rule>();
		var rule = ruleParser.Parse(tokenReader);
		while(rule != null)
		{
			rules.Add(rule);
			if(rule.SemiColon is null)
				break;

			rule = ruleParser.Parse(tokenReader);
		}

		var closeBrace = tokenReader.RequireSymbol(Symbol.CloseBrace);
		return new Block(openBrace, rules, closeBrace);
	}
}
