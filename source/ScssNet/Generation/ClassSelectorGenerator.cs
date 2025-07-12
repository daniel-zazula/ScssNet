using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class ClassSelectorGenerator(Lazy<CompoundSelectorGenerator> compoundSelectorGenerator)
{
	public void Generate(ClassSelector classSelector, TextWriter writer)
	{
		writer.Write('.');
		writer.Write(classSelector.Identifier);
		if (classSelector.Qualifier is not null)
			compoundSelectorGenerator.Value.Generate(classSelector.Qualifier, writer);
	}
}
