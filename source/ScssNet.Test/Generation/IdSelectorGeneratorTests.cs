using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class IdSelectorGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteIdSelector()
	{
		var hash = CreateHashValueToken("#myid");
		var idSelector = new IdSelector(hash, null);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(idSelector, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("#myid");
	}
}
