using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorListParserTests : ParserTestBase
{
	[TestMethod]
	[DynamicData(nameof(BuildSelectorPermutations))]
	public void ShouldParseSelectorList(string[] selectorsSource)
	{
		var source = string.Join(", ", selectorsSource);
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorListParser = provider.GetRequiredService<SelectorListParser>();

		var selectorList = selectorListParser.Parse(tokenReader);
		selectorList.ShouldNotBeNull();
		var selectors = selectorList.Items;
		selectors.Count.ShouldBe(4);

		for(var i = 0; i < selectorsSource.Length; i++)
		{
			var selectorSource = selectorsSource[i];
			var item = selectorList.Items.ElementAt(i);
			item.Selector.AssertText(selectorSource);
		}

		selectorList.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		var CompositeSelectorQualifiers = new string[]
		{
			TestSelectors.AttributeSelector, TestSelectors.IdSelector, TestSelectors.ClassSelector,
			TestSelectors.AttributeSelector
		};

		return new Permutations<string>(CompositeSelectorQualifiers)
			.Select(p => new object[] { p.ToArray() });
	}
}
