using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier, ISelectorQualifier? qualifier)
	{
		public IdentifierToken Identifier { get; } = identifier;
		public ISelectorQualifier? Qualifier { get; } = qualifier;
	}

	internal class TagSelectorParser(Lazy<SelectorQualifierParser> selectorQualifierParser)
	{
		internal TagSelector? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken identifier)
				return null;

			tokenReader.Read();
			return new TagSelector(identifier, selectorQualifierParser.Value.Parse(tokenReader));
		}
	}
}
