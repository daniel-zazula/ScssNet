using ScssNet.Structures;

namespace ScssNet.Generation;

internal class TagSelectorGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(TagSelector tagSelector, CssWriter writer)
	{
		writer.Write(tagSelector.Identifier);

		if(tagSelector.Qualifier is not null)
			selectorGenerator.Value.Generate(tagSelector.Qualifier, writer);
	}
}
