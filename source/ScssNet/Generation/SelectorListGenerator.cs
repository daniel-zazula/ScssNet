using ScssNet.Structures;

namespace ScssNet.Generation;

internal class SelectorListGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(SelectorList selectorList, CssWriter writer)
	{
		foreach(var item in selectorList.Items)
		{
			selectorGenerator.Value.Generate(item.Selector, writer);
			if(item.Comma != null)
			{
				writer.Write(item.Comma);
			}
		}
	}
}
