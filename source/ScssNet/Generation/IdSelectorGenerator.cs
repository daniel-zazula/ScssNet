using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class IdSelectorGenerator
(
	TokenGenerator tokenGenerator, Lazy<CompoundSelectorGenerator> compoundSelectorGenerator
)
{
	public void Generate(IdSelector idSelector, TextWriter writer)
	{
		tokenGenerator.Generate(idSelector.Hash, writer);
		tokenGenerator.Generate(idSelector.Identifier, writer);
		if(idSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(idSelector.Qualifier, writer);
	}
}
