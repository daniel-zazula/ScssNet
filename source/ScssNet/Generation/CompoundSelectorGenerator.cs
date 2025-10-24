using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class CompoundSelectorGenerator
(
	Lazy<IdSelectorGenerator> idSelectorGenerator, Lazy<ClassSelectorGenerator> classSelectorGenerator,
	Lazy<AttributteSelectorGenerator> attributteSelectorGenerator
)
{
	public void Generate(ICompoundSelector compoundSelector, CssWriter writer)
	{
		switch(compoundSelector)
		{
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
