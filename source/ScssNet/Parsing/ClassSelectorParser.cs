using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ClassSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
{
	internal ClassSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
	{
		var dot = tokenReader.Match(Symbol.Hash, skipWhitespace);
		if(dot is null)
			return null;

		var identifier = tokenReader.RequireIdentifier(false);
		var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

		return new ClassSelector(dot, identifier, compoundSelector);
	}
}
