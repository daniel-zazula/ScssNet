using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;

namespace ScssNet.Test.Generation;

[TestClass]
public class AttributeSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedAttributeSelector = "[attr=\"some-value\"]";

	[TestMethod]
	public void ShouldGenerateFromAttributeSelectorGenerator()
	{
		var attributeSelector = AttributeSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<AttributeSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(attributeSelector, cssWriter);

		AssertAttributeSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var attributeSelector = AttributeSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(attributeSelector, cssWriter);

		AssertAttributeSelector(provider);
	}

	private static void AssertAttributeSelector(ServiceProvider provider)
	{
		provider.GetStringWriter().ShouldContain(ExpectedAttributeSelector);
	}
}
