using ScssNet.Structures;

namespace ScssNet.Generation;

internal class IdSelectorGenerator(Lazy<SelectorGenerator> selectorGenerator)
{
	public void Generate(IdSelector idSelector, CssWriter writer)
	{
		writer.Write(idSelector.Identifier);

		if(idSelector.Qualifier is not null)
			selectorGenerator.Value.Generate(idSelector.Qualifier, writer);
	}
}
