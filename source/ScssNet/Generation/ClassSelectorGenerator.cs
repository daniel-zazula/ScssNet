using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class ClassSelectorGenerator
(
	TokenGenerator tokenGenerator, Lazy<CompoundSelectorGenerator> compoundSelectorGenerator
)
{
	public void Generate(ClassSelector classSelector, TextWriter writer)
	{
		tokenGenerator.Generate(classSelector.Dot, writer);
		tokenGenerator.Generate(classSelector.Identifier, writer);
		if (classSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(classSelector.Qualifier, writer);
	}
}
