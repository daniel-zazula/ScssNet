using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class IdSelectorParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseIdSelector()
	{
		var source = "#my-id";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var idSelectorParser = provider.GetRequiredService<IdSelectorParser>();

		var idSelector = idSelectorParser.Parse(tokenReader);
		idSelector.ShouldNotBeNull();
		idSelector!.Identifier?.Text.ShouldBe("my-id");
		idSelector.Qualifier.ShouldBeNull();
		idSelector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
