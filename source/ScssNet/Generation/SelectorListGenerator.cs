using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class SelectorListGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(SelectorList selectorList, CssWriter writer)
	{
		var firstItem = true;
		foreach(var selector in selectorList.Selectors)
		{
			if(!firstItem)
			{
				writer.Write(", ");
			}
			selectorGenerator.Value.Generate(selector, writer);
			firstItem = false;
		}
	}
}
