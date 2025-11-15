using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ClassSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
{
	internal ClassSelector? Parse(ITokenReader tokenReader)
	{
		var dot = tokenReader.Match(Symbol.Hash);
		if(dot is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();
		var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

		return new ClassSelector(dot, identifier, compoundSelector);
	}
}
