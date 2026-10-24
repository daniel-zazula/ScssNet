using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtRuleGenerator(GeneratorContext generatorContext): IGenerator<IAtRule>
{
	private IGenerator<AtCharset> AtCharsetGenerator => generatorContext.GetGenerator<AtCharset>();
	private IGenerator<AtImport> AtImportGenerator => generatorContext.GetGenerator<AtImport>();
	private IGenerator<AtMedia> AtMediaGenerator => generatorContext.GetGenerator<AtMedia>();
	private IGenerator<AtNamespace> AtNamespaceGenerator => generatorContext.GetGenerator<AtNamespace>();

	public void Generate(IAtRule atRule, CssWriter writer)
	{
		switch(atRule) {
			case AtCharset atCharset:
				AtCharsetGenerator.Generate(atCharset, writer);
				break;
			case AtImport atImport:
				AtImportGenerator.Generate(atImport, writer);
				break;
			case AtMedia atMedia:
				AtMediaGenerator.Generate(atMedia, writer);
				break;
			case AtNamespace atNamespace:
				AtNamespaceGenerator.Generate(atNamespace, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for AtRule type {atRule.GetType().Name}.");
		}
	}
}
