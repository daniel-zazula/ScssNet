using ScssNet.Structures;

namespace ScssNet.Generation;

internal class UniversalSelectorGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(UniversalSelector universalSelector, CssWriter writer)
	{
		writer.Write(universalSelector.Asterisk);

		if(universalSelector.Qualifier is not null)
			selectorGenerator.Value.Generate(universalSelector.Qualifier, writer);
	}
}
