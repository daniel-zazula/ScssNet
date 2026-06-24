using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class PseudoElementSelectorParserTests: ParserTestBase
{
	[TestMethod]
	[DataRow(Selectors.PseudoElementSelector)]
	[DataRow("::-my-element")]
	public void ShouldParsePseudoElementSelector(string source)
	{
		var text = source[2..]; // Remove leading double colon
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var parser = provider.GetRequiredService<PseudoElementSelectorParser>();

		var selector = parser.Parse(tokenReader);
		selector.ShouldNotBeNull();
		selector.AssertIdentifierText(text);
		selector.Qualifier.ShouldBeNull();
		selector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
