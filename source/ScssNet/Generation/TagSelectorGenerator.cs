using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class TagSelectorGenerator
(
	TokenGenerator tokenGenerator, Lazy<CompoundSelectorGenerator> compoundSelectorGenerator
)
{
	public void Generate(TagSelector tagSelector, TextWriter writer)
	{
		tokenGenerator.Generate(tagSelector.Identifier, writer);
		if(tagSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(tagSelector.Qualifier, writer);
	}
}
