using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorListParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseSelectorList()
	{
		var source = ".my-class, div, [href], #theId";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorListParser = provider.GetRequiredService<SelectorListParser>();

		var selectorList = selectorListParser.Parse(tokenReader);
		selectorList.ShouldNotBeNull();
		selectorList.Selectors?.Count.ShouldBe(4);

		selectorList.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
