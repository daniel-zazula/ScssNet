using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AttributeSelectorGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedAttributeSelector = "[attr=\"some-value\"]";

	[TestMethod]
	public void ShouldGenerateFromAttributeSelectorGenerator()
	{
		var attributeSelector = CreateAttributeSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<AttributeSelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(attributeSelector, cssWriter);

		AssertAttributeSelector(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromSelectorGenerator()
	{
		var attributeSelector = CreateAttributeSelector();

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(attributeSelector, cssWriter);

		AssertAttributeSelector(provider);
	}

	internal static AttributeSelector CreateAttributeSelector(int previousColumnNumber = 0)
	{
		var openBracket = CreateSymbolToken(Symbol.OpenBracket, columnNumber: previousColumnNumber + 1);
		var attributeIdentifier = CreateIdentifierToken("attr", columnNumber: openBracket.End.ColumnNumber + 1);
		var equalSign = CreateSymbolToken(Symbol.Equals, columnNumber: attributeIdentifier.End.ColumnNumber + 1);
		var value = CreateStringToken(@"""some-value""", columnNumber: equalSign.End.ColumnNumber + 1);
		var closeBracket = CreateSymbolToken(Symbol.CloseBracket, columnNumber: value.End.ColumnNumber + 1);

		return new AttributeSelector(openBracket, attributeIdentifier, equalSign, value, null, closeBracket, null);
	}

	private static void AssertAttributeSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedAttributeSelector);
	}
}
