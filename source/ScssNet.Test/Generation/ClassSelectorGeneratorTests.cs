using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class ClassSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedClassSelector = ".my-class";

	[TestMethod]
	public void ShouldGenerateFromClassSelectorGenerator()
	{
		var classSelector = CreateClassSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<ClassSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(classSelector, cssWriter);

		AssertClassSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var classSelector = CreateClassSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(classSelector, cssWriter);

		AssertClassSelector(provider);
	}

	internal static ClassSelector CreateClassSelector()
	{
		var dot = CreateSymbolToken(Symbol.Dot);
		var identifier = CreateIdentifierToken("my-class", columnNumber: dot.End.ColumnNumber + 1);

		return new ClassSelector(dot, identifier, null);
	}

	private static void AssertClassSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedClassSelector);
	}
}
