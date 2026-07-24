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
	[TestMethod]
	public void ShouldWriteClassSelector()
	{
		var dot = CreateSymbolToken(Symbol.Dot);
		var identifier = CreateIdentifierToken("my-class", columnNumber: dot.End.ColumnNumber + 1);

		var classSelector = new ClassSelector(dot, identifier, null);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(classSelector, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(".my-class");
	}
}
