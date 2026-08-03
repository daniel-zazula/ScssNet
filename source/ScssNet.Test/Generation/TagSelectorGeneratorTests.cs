using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class TagSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedTagSelector = "h2";

	[TestMethod]
	public void ShouldGenerateFromTagSelectorGenerator()
	{
		var tagSelector = CreateTagSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<TagSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(tagSelector, cssWriter);

		AssertTagSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var tagSelector = CreateTagSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(tagSelector, cssWriter);

		AssertTagSelector(provider);
	}

	internal static TagSelector CreateTagSelector(int previousColumnNumber = 0)
	{
		var identifier = CreateIdentifierToken("h2", columnNumber: previousColumnNumber + 1);
		return new TagSelector(identifier, null);
	}

	private static void AssertTagSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedTagSelector);
	}
}
