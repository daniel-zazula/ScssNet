using ScssNet.Structures;

namespace ScssNet.Generation;

internal class AtRuleGenerator(Lazy<AtCharsetGenerator> atCharsetGenerator, Lazy<AtImportGenerator> atImportGenerator, Lazy<AtMediaGenerator> atMediaGenerator)
{
	public void Generate(IAtRule atRule, CssWriter writer)
	{
		switch(atRule) {
			case AtCharset charset:
				atCharsetGenerator.Value.Generate(charset, writer);
				break;
			case AtImport import:
				atImportGenerator.Value.Generate(import, writer);
				break;
			case AtMedia media:
				atMediaGenerator.Value.Generate(media, writer);
				break;
			default:
				throw new NotSupportedException($"No generator found for AtRule type {atRule.GetType().Name}.");
		}
	}
}
