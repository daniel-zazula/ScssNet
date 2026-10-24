using ScssNet.Structures;

namespace ScssNet.Generation;

internal class IdSelectorGenerator(GeneratorContext generatorContext): IGenerator<IdSelector>
{
	private IGenerator<ISelector> SelectorGenerator => generatorContext.GetGenerator<ISelector>();

	public void Generate(IdSelector idSelector, CssWriter writer)
	{
		writer.Write(idSelector.Identifier);

		if(idSelector.Qualifier is not null)
			SelectorGenerator.Generate(idSelector.Qualifier, writer);
	}
}
