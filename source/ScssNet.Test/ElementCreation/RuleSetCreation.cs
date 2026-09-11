using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class RuleSetCreation
{
	extension(RuleSet)
	{
		internal static RuleSet Create()
		{
			var selector = new TagSelector(IdentifierToken.Create("p"), null);
			var selectors = new SelectorList([new SelectorListItem(selector, null)]);
			var block = Block.Create(predecessor: selectors.Items.Last());
			return new RuleSet(selectors, block);
		}
	}
}
