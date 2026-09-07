using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtMediaGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	[DynamicData(nameof(GetMediaTypesData))]
	public void ShouldGenerateMediaTypeFromAtRuleGenerator(MediaQueryTypeKeyword mediaType)
	{
		var atMedia = CreateAtMedia(mediaType);

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider, MediaQueryGeneratorTests.GetExpected(mediaType));
	}

	[TestMethod]
	[DynamicData(nameof(GetMediaTypesData))]
	public void ShouldGenerateMediaTypeFromAtMediaGenerator(MediaQueryTypeKeyword mediaType)
	{
		var atMedia = CreateAtMedia(mediaType);

		var provider = BuildServiceProvider();
		var atMediaGenerator = provider.GetRequiredService<AtMediaGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atMediaGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider, MediaQueryGeneratorTests.GetExpected(mediaType));
	}

	[TestMethod]
	[DynamicData(nameof(GetUnaryExpressionData))]
	public void ShouldGenerateUnaryExpressionFromAtRuleGenerator
	(
		MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType
	)
	{
		var atMedia = CreateAtMedia(mediaOperator, mediaType);

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider, MediaQueryGeneratorTests.GetExpected(mediaOperator, mediaType));
	}

	[TestMethod]
	[DynamicData(nameof(GetUnaryExpressionData))]
	public void ShouldGenerateUnaryExpressionFromAtMediaGenerator
	(
		MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType
	)
	{
		var atMedia = CreateAtMedia(mediaOperator, mediaType);

		var provider = BuildServiceProvider();
		var atMediaGenerator = provider.GetRequiredService<AtMediaGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atMediaGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider, MediaQueryGeneratorTests.GetExpected(mediaOperator, mediaType));
	}

	public static IEnumerable<object[]> GetMediaTypesData() => MediaQueryGeneratorTests.GetMediaTypesData();

	public static IEnumerable<object[]> GetUnaryExpressionData() => MediaQueryGeneratorTests.GetUnaryExpressionData();

	internal static AtMedia CreateAtMedia(MediaQueryTypeKeyword mediaType)
	{
		return CreateAtMedia(CreateMediaQuery);

		IMediaQuery CreateMediaQuery(ISourceElement? predecessor)
		{
			return MediaQueryGeneratorTests.CreateMediaQueryType(mediaType, predecessor: predecessor);
		}
	}

	internal static AtMedia CreateAtMedia(MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType)
	{
		return CreateAtMedia(CreateMediaQuery);

		IMediaQuery CreateMediaQuery(ISourceElement? predecessor)
		{
			return MediaQueryGeneratorTests.CreateMediaQueryUnaryExpression
			(
				mediaOperator, mediaType, predecessor: predecessor
			);
		}
	}

	internal static AtMedia CreateAtMedia(Func<ISourceElement?, IMediaQuery> createMediaQuery)
	{
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateAtKeywordToken(AtKeyword.Media, predecessor: at);

		var mediaQuery = createMediaQuery(keyword);

		var block = BlockGeneratorTests.CreateBlock(predecessor: mediaQuery);

		return new AtMedia(at, keyword, mediaQuery, block);
	}

	internal static void AssertAtMedia(ServiceProvider provider, string expectedMediaQueryText)
	{
		var expected = $"@media {expectedMediaQueryText} {BlockGeneratorTests.ExpectedBlock}";

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}
}
