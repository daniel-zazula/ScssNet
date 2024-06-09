using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class SelectorList(ICollection<ISelector> selectors): ISourceElement
	{
		public ICollection<ISelector> Selectors => selectors;

		public IEnumerable<Issue> Issues => selectors.ConcatIssues();

		public SourceCoordinates Start => Selectors.First().Start;

		public SourceCoordinates End => Selectors.Last().End;
	}

	internal class SelectorListParser(Lazy<SelectorParser> selectorParser)
	{
		internal SelectorList? Parse(TokenReader tokenReader)
		{
			var selector = ParseSelector(selectorParser, tokenReader);
			if(selector == null)
				return null;

			var selectors = new List<ISelector>();
			while(selector != null)
			{
				selectors.Add(selector);
				selector = ParseSelector(selectorParser, tokenReader);
			}

			return new SelectorList([.. selectors]);

			static ISelector? ParseSelector(Lazy<SelectorParser> selectorParser, TokenReader tokenReader) => selectorParser.Value.Parse(tokenReader);
		}
	}
}
