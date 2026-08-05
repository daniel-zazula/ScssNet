using ScssNet.Structures;

namespace ScssNet.Generation;

internal class ClassSelectorGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(ClassSelector classSelector, CssWriter writer)
	{
		writer.Write(classSelector.Dot);
		writer.Write(classSelector.Identifier);

		if (classSelector.Qualifier is not null)
			selectorGenerator.Value.Generate(classSelector.Qualifier, writer);
	}
}
