using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class PseudoClassSelectorParserTests: ParserTestBase
{
	[TestMethod]
	[DataRow(Selectors.PseudoClassSelector)]
	[DataRow(":-name-with-dash")]
	public void ShouldParsePseudoClassSelector(string source)
	{
		var text = source[1..]; // Remove leading colon
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var parser = provider.GetRequiredService<PseudoClassSelectorParser>();

		var selector = parser.Parse(tokenReader);
		selector.ShouldNotBeNull();
		selector.AssertIdentifierText(text);
		selector.Qualifier.ShouldBeNull();
		selector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
