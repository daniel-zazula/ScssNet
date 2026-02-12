using ScssNet.SourceElements;

namespace ScssNet.Generation;

internal class CompoundSelectorGenerator
(
	Lazy<IdSelectorGenerator> idSelectorGenerator, Lazy<ClassSelectorGenerator> classSelectorGenerator,
	Lazy<AttributeSelectorGenerator> attributeSelectorGenerator
)
{
	public void Generate(ISelectorQualifier compoundSelector, CssWriter writer)
	{
		switch(compoundSelector)
		{
			case IdSelector idSelector:
				idSelectorGenerator.Value.Generate(idSelector, writer);
				break;
			case ClassSelector classSelector:
				classSelectorGenerator.Value.Generate(classSelector, writer);
				break;
			case AttributeSelector attributeSelector:
				attributeSelectorGenerator.Value.Generate(attributeSelector, writer);
				break;
		}
	}
}
