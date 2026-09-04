using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class IdSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedIdSelector = "#myid";

	[TestMethod]
	public void ShouldGenerateFromIdSelectorGenerator()
	{
		var idSelector = CreateIdSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<IdSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(idSelector, cssWriter);

		AssertIdSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var idSelector = CreateIdSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(idSelector, cssWriter);

		AssertIdSelector(provider);
	}

	internal static IdSelector CreateIdSelector(ISourceElement? predecessor = null)
	{
		var hash = CreateHashValueToken("#myid", predecessor: predecessor);
		return new IdSelector(hash, null);
	}

	private static void AssertIdSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedIdSelector);
	}
}
