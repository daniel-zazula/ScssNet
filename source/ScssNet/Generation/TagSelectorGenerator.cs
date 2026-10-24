using ScssNet.Structures;

namespace ScssNet.Generation;

internal class TagSelectorGenerator(GeneratorContext generatorContext): IGenerator<TagSelector>
{
	private IGenerator<ISelector> SelectorGenerator => generatorContext.GetGenerator<ISelector>();

	public void Generate(TagSelector tagSelector, CssWriter writer)
	{
		writer.Write(tagSelector.Identifier);

		if(tagSelector.Qualifier is not null)
			SelectorGenerator.Generate(tagSelector.Qualifier, writer);
	}
}
