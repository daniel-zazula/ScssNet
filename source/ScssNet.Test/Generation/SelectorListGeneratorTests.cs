using System;
using System.IO;
using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

using AttributeTests = ScssNet.Test.Generation.AttributeSelectorGeneratorTests;
using ClassTests = ScssNet.Test.Generation.ClassSelectorGeneratorTests;
using IdTests = ScssNet.Test.Generation.IdSelectorGeneratorTests;
using TagTests = ScssNet.Test.Generation.TagSelectorGeneratorTests;

namespace ScssNet.Test.Generation;

[TestClass]
public class SelectorListGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	[DynamicData(nameof(BuildSelectorPermutations))]
	public void ShouldGenerateSelectorList(string[] expectedSelectors)
	{
		var items = new List<SelectorListItem>();
		var previousColumn = 0;
		var lastSelector = expectedSelectors.Last();
		foreach(var expectedSelector in expectedSelectors)
		{
			ISelector selector = expectedSelector switch
			{
				AttributeTests.ExpectedAttributeSelector => AttributeTests.CreateAttributeSelector(previousColumn),
				ClassTests.ExpectedClassSelector => ClassTests.CreateClassSelector(previousColumn),
				IdTests.ExpectedIdSelector => IdTests.CreateIdSelector(previousColumn),
				TagTests.ExpectedTagSelector => TagTests.CreateTagSelector(previousColumn),
				_ => throw new InvalidOperationException($"Unexpected selector: {expectedSelector}")
			};

			var comma = expectedSelector != lastSelector
				? CreateSymbolToken(Symbol.Comma, columnNumber: selector.End.ColumnNumber + 1)
				: null;
			var item = new SelectorListItem(selector, comma);

			previousColumn = item.End.ColumnNumber;

			items.Add(item);
		}

		var list = new SelectorList(items);

		var provider = BuildServiceProvider();
		var selectorListGenerator = provider.GetRequiredService<SelectorListGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		selectorListGenerator.Generate(list, writer);

		provider.GetRequiredService<StringWriter>().ToString().ShouldBe(string.Join(",", expectedSelectors));
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		var Selectors = new string[]
		{
			AttributeTests.ExpectedAttributeSelector, ClassTests.ExpectedClassSelector, IdTests.ExpectedIdSelector,
			TagTests.ExpectedTagSelector
		};

		return new Permutations<string>(Selectors).Select(p => new object[] { p.ToArray() });
	}
}
