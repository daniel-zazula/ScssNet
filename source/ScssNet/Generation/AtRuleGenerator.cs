using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtRuleGenerator
(
	Lazy<AtCharsetGenerator> atCharsetGenerator, Lazy<AtImportGenerator> atImportGenerator,
	Lazy<AtMediaGenerator> atMediaGenerator, Lazy<AtNamespaceGenerator> atNamespaceGenerator
)
{
	public void Generate(IAtRule atRule, CssWriter writer)
	{
		switch(atRule) {
			case AtCharset atCharset:
				atCharsetGenerator.Value.Generate(atCharset, writer);
				break;
			case AtImport atImport:
				atImportGenerator.Value.Generate(atImport, writer);
				break;
			case AtMedia atMedia:
				atMediaGenerator.Value.Generate(atMedia, writer);
				break;
			case AtNamespace atNamespace:
				atNamespaceGenerator.Value.Generate(atNamespace, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for AtRule type {atRule.GetType().Name}.");
		}
	}
}
