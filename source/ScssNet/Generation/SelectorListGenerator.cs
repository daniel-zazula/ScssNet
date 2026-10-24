using ScssNet.Structures;

namespace ScssNet.Generation;

internal class SelectorListGenerator(GeneratorContext generatorContext): IGenerator<SelectorList>
{
	private IGenerator<ISelector> SelectorGenerator => generatorContext.GetGenerator<ISelector>();

	public void Generate(SelectorList selectorList, CssWriter writer)
	{
		foreach(var item in selectorList.Items)
		{
			SelectorGenerator.Generate(item.Selector, writer);
			if(item.Comma != null)
			{
				writer.Write(item.Comma);
			}
		}
	}
}
