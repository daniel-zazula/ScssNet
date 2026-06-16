using System;
using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorListParserTests : ParserTestBase
{
	private static readonly string[] CompositeSelectorQualifiers = [Selectors.AttributeSelector, Selectors.IdSelector, Selectors.ClassSelector, Selectors.AttributeSelector];
	internal static IEnumerable<object[]> CompositeSelectorQualifierParams => BuildSelectorPermutations();

	[TestMethod]
	[DynamicData(nameof(CompositeSelectorQualifierParams))]
	public void ShouldParseSelectorList(string[] selectorsSource)
	{
		var source = string.Join(", ", selectorsSource);
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorListParser = provider.GetRequiredService<SelectorListParser>();

		var selectorList = selectorListParser.Parse(tokenReader);
		selectorList.ShouldNotBeNull();
		var selectors = selectorList.Selectors;
		selectors.Count.ShouldBe(4);

		for(var i = 0; i < selectorsSource.Length; i++)
		{
			var selectorSource = selectorsSource[i];
			var selector = selectorList.Selectors.ElementAt(i);
			selector.Assert(selectorSource);
		}

		selectorList.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		return new Permutations<string>(CompositeSelectorQualifiers)
			.Select(p => new object[] { p.ToArray() });
	}
}
