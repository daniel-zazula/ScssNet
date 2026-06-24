using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class IdSelectorParserTests : ParserTestBase
{
	[TestMethod]
	[DataRow(Selectors.IdSelector)]
	[DataRow("#-id-with-dash")]
	public void ShouldParseIdSelector(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var idSelectorParser = provider.GetRequiredService<IdSelectorParser>();

		var idSelector = idSelectorParser.Parse(tokenReader);
		idSelector.ShouldNotBeNull();
		idSelector.AssertIdentifierValue(source);
		idSelector.Qualifier.ShouldBeNull();
		idSelector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
