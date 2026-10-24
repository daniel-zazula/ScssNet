using ScssNet.Structures;

namespace ScssNet.Generation;

internal class SelectorGenerator(GeneratorContext generatorContext): IGenerator<ISelector>
{
	private IGenerator<TagSelector> TagSelectorGenerator => generatorContext.GetGenerator<TagSelector>();
	private IGenerator<IdSelector> IdSelectorGenerator => generatorContext.GetGenerator<IdSelector>();
	private IGenerator<ClassSelector> ClassSelectorGenerator => generatorContext.GetGenerator<ClassSelector>();
	private IGenerator<AttributeSelector> AttributeSelectorGenerator => generatorContext.GetGenerator<AttributeSelector>();

	public void Generate(ISelector selector, CssWriter writer)
	{
		switch(selector)
		{
			case UniversalSelector universalSelector:
				writer.Write(universalSelector.Asterisk);
				break;
			case TagSelector tagSelector:
				TagSelectorGenerator.Generate(tagSelector, writer);
				break;
			case IdSelector idSelector:
				IdSelectorGenerator.Generate(idSelector, writer);
				break;
			case ClassSelector classSelector:
				ClassSelectorGenerator.Generate(classSelector, writer);
				break;
			case AttributeSelector attributeSelector:
				AttributeSelectorGenerator.Generate(attributeSelector, writer);
				break;
		}
	}
}
