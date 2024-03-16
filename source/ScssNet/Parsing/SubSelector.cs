using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class SubSelector(ICompoundSelector parentSelector, ICompoundSelector childSelector)
	{
		public ICompoundSelector ParentSelector => parentSelector;
		public ICompoundSelector ChildSelector => childSelector;
	}

	internal class SubSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal SubSelector? Parse(TokenReader tokenReader, ICompoundSelector parentSelector)
		{
			var selector = compoundSelectorParser.Value.Parse(tokenReader);
			if (selector == null)
				return null;

			return new SubSelector(selector, parentSelector);
		}
	}
}
