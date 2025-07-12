using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class IdSelectorGenerator(Lazy<CompoundSelectorGenerator> compoundSelectorGenerator)
{
	public void Generate(IdSelector idSelector, TextWriter writer)
	{
		writer.Write('#');
		writer.Write(idSelector.Identifier);
		if(idSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(idSelector.Qualifier, writer);
	}
}
