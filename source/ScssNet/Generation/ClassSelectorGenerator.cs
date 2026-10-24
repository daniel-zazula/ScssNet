using ScssNet.Structures;

namespace ScssNet.Generation;

internal class ClassSelectorGenerator(GeneratorContext generatorContext): IGenerator<ClassSelector>
{
	private IGenerator<ISelector> SelectorGenerator => generatorContext.GetGenerator<ISelector>();

	public void Generate(ClassSelector classSelector, CssWriter writer)
	{
		writer.Write(classSelector.Dot);
		writer.Write(classSelector.Identifier);

		if (classSelector.Qualifier is not null)
			SelectorGenerator.Generate(classSelector.Qualifier, writer);
	}
}
