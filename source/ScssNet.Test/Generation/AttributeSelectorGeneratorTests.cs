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

	internal static AttributeSelector CreateAttributeSelector(ISourceElement? predecessor = null)
	{
		var openBracket = CreateSymbolToken(Symbol.OpenBracket, predecessor: predecessor);
		var attributeIdentifier = CreateIdentifierToken("attr", predecessor: openBracket);
		var equalSign = CreateSymbolToken(Symbol.Equals, predecessor: attributeIdentifier);
		var value = CreateStringToken(@"""some-value""", predecessor: equalSign);
		var closeBracket = CreateSymbolToken(Symbol.CloseBracket, predecessor: value);

		return new AttributeSelector(openBracket, attributeIdentifier, equalSign, value, null, closeBracket, null);
	}

	private static void AssertAttributeSelector(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedAttributeSelector);
	}
}
