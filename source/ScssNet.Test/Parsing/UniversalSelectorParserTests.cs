using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class UniversalSelectorParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseUniversalSelector()
	{
		var source = TestSelectors.UniversalSelector;
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var universalSelectorParser = provider.GetRequiredService<UniversalSelectorParser>();

		var universalSelector = universalSelectorParser.Parse(tokenReader);
		universalSelector.ShouldNotBeNull();
		universalSelector.Qualifier.ShouldBeNull();
		universalSelector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
