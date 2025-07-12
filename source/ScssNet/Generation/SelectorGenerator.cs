using System.IO;
using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class SelectorGenerator
(
	Lazy<TagSelectorGenerator> tagSelectorGenerator, Lazy<IdSelectorGenerator> idSelectorGenerator,
	Lazy<ClassSelectorGenerator> classSelectorGenerator, Lazy<AttributteSelectorGenerator> attributteSelectorGenerator
)
{
	public void Generate(ISelector selector, TextWriter writer)
	{
		switch(selector)
		{
			case TagSelector tagSelector:
				tagSelectorGenerator.Value.Generate(tagSelector, writer);
				break;
			case IdSelector idSelector:
				idSelectorGenerator.Value.Generate(idSelector, writer);
				break;
			case ClassSelector classSelector:
				classSelectorGenerator.Value.Generate(classSelector, writer);
				break;
			case AttributteSelector attributteSelector:
				attributteSelectorGenerator.Value.Generate(attributteSelector, writer);
				break;
		}
	}
}
