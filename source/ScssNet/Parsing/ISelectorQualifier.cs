using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface ISelectorQualifier
	{
		ISelectorQualifier? Qualifier { get; }
	}

	internal class SelectorQualifierParser(Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser)
	{
		internal ISelectorQualifier? Parse(TokenReader tokenReader)
		{
			return (ISelectorQualifier?)idSelectorParser.Value.Parse(tokenReader, false) ?? classSelectorParser.Value.Parse(tokenReader, false);
		}
	}
}
