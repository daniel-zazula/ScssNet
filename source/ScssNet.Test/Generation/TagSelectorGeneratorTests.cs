using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class TagSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedTagSelector = "h2";

	[TestMethod]
	public void ShouldGenerateFromTagSelectorGenerator()
	{
		var tagSelector = TagSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<TagSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(tagSelector, cssWriter);

		AssertTagSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var tagSelector = TagSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(tagSelector, cssWriter);

		AssertTagSelector(provider);
	}

	private static void AssertTagSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedTagSelector);
	}
}
