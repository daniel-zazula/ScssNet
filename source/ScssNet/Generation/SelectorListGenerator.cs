using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class SelectorListGenerator(SelectorGenerator selectorGenerator)
{
	public void Generate(SelectorList selectorList, TextWriter writer)
	{
		var firstItem = true;
		foreach(var selector in selectorList.Selectors)
		{
			if(!firstItem)
			{
				writer.Write(", ");
			}
			selectorGenerator.Generate(selector, writer);
			firstItem = false;
		}
	}
}
