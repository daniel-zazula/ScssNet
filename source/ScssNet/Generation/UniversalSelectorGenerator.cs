using ScssNet.Structures;

namespace ScssNet.Generation;

internal class UniversalSelectorGenerator(GeneratorContext generatorContext): IGenerator<UniversalSelector>
{
	private IGenerator<ISelector> SelectorGenerator => generatorContext.GetGenerator<ISelector>();

	public void Generate(UniversalSelector universalSelector, CssWriter writer)
	{
		writer.Write(universalSelector.Asterisk);

		if(universalSelector.Qualifier is not null)
			SelectorGenerator.Generate(universalSelector.Qualifier, writer);
	}
}
