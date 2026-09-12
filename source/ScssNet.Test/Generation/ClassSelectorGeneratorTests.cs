using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;

namespace ScssNet.Test.Generation;

[TestClass]
public class ClassSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedClassSelector = ".my-class";

	[TestMethod]
	public void ShouldGenerateFromClassSelectorGenerator()
	{
		var classSelector = ClassSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ClassSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(classSelector, cssWriter);

		AssertClassSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var classSelector = ClassSelector.Create();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(classSelector, cssWriter);

		AssertClassSelector(provider);
	}

	private static void AssertClassSelector(ServiceProvider provider)
	{
		provider.GetStringWriter().ShouldContain(ExpectedClassSelector);
	}
}
