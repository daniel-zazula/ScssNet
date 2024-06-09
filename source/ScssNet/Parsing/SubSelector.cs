using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class SubSelector(ISelector parentSelector, ISelector childSelector): ISourceElement
	{
		public ISelector ParentSelector => parentSelector;
		public ISelector ChildSelector => childSelector;

		public IEnumerable<Issue> Issues => SourceElement.List(parentSelector, childSelector).ConcatIssues();

		public SourceCoordinates Start => parentSelector.Start;

		public SourceCoordinates End => childSelector.End;
	}

	internal class SubSelectorParser(Lazy<SelectorParser> selectorParser)
	{
		internal SubSelector? Parse(TokenReader tokenReader, ISelector parentSelector)
		{
			var selector = selectorParser.Value.Parse(tokenReader);
			if (selector == null)
				return null;

			return new SubSelector(selector, parentSelector);
		}
	}
}
