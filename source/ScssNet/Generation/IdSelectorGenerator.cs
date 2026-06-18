using ScssNet.Structures;

namespace ScssNet.Generation;

internal class IdSelectorGenerator(Lazy<CompoundSelectorGenerator> compoundSelectorGenerator)
{
	public void Generate(IdSelector idSelector, CssWriter writer)
	{
		writer.Write(idSelector.Id);

		if(idSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(idSelector.Qualifier, writer);
	}
}
