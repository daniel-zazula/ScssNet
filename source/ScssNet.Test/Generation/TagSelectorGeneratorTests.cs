using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class TagSelectorGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteIdentifier()
	{
		var identifier = CreateIdentifierToken("h2");
		var tagSelector = new TagSelector(identifier, null);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(tagSelector, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("h2");
	}
}
