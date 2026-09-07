using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class MediaQueryGeneratorTests : GeneratorTestBase
{
	[TestMethod]
	[DynamicData(nameof(GetMediaTypesData))]
	public void ShouldGenerateMediaQueryTypeKeyword(MediaQueryTypeKeyword mediaType)
	{
		var mediaTypeText = mediaType.ToString().ToLower();
		var mediaQuery = CreateMediaQueryType(mediaType, mediaTypeText);

		var provider = BuildServiceProvider();
		var mediaQueryGenerator = provider.GetRequiredService<MediaQueryGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		mediaQueryGenerator.Generate(mediaQuery, writer);

		var expected = GetExpected(mediaType);
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}

	[TestMethod]
	[DynamicData(nameof(GetUnaryExpressionData))]
	public void ShouldGenerateMediaQueryTypeKeyword
	(
		MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType
	)
	{
		var operatorText = mediaOperator.ToString().ToLower();
		var typeText = mediaType.ToString().ToLower();
		var mediaQuery = CreateMediaQueryUnaryExpression(mediaOperator, mediaType, operatorText, typeText);

		var provider = BuildServiceProvider();
		var mediaQueryGenerator = provider.GetRequiredService<MediaQueryGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		mediaQueryGenerator.Generate(mediaQuery, writer);

		var expected = GetExpected(mediaOperator, mediaType);
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}

	public static IEnumerable<object[]> GetMediaTypesData()
	{
		var mediaTypes = Enum.GetValues<MediaQueryTypeKeyword>();

		foreach(var mediaType in mediaTypes)
		{
			yield return new object[] { mediaType };
		}
	}

	public static IEnumerable<object[]> GetUnaryExpressionData()
	{
		var mediaOperators = Enum.GetValues<MediaQueryOperatorKeyword>();
		var mediaTypes = Enum.GetValues<MediaQueryTypeKeyword>();

		foreach(var mediaOperator in mediaOperators)
		{
			foreach(var mediaType in mediaTypes)
			{
				yield return new object[] { mediaOperator, mediaType };
			}
		}
	}

	internal static MediaQueryTypeKeywordToken CreateMediaQueryType
	(
		MediaQueryTypeKeyword type, string? text = null, ISourceElement? predecessor = null
	)
	{
		text = PrepareKeywordValue(type, text);
		var length = text.Length;
		var span = CreateSpan(predecessor, length);

		return new MediaQueryTypeKeywordToken(type, text, span, Separator.Empty, Separator.Empty);
	}

	internal static MediaQueryUnaryExpression CreateMediaQueryUnaryExpression
	(
		MediaQueryOperatorKeyword operatorKeyword, MediaQueryTypeKeyword typeKeyword, 
		string? operatorText = null, string? typeText = null,
		ISourceElement? predecessor = null
	)
	{
		var operatorToken = CreateMediaQueryOperator(operatorKeyword, operatorText, predecessor);
		var typeToken = CreateMediaQueryType(typeKeyword, typeText, operatorToken);

		return new MediaQueryUnaryExpression(operatorToken, typeToken);
	}

	internal static string GetExpected(MediaQueryTypeKeyword mediaType)
	{
		return mediaType.ToString().ToLower();
	}

	internal static string GetExpected(MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType)
	{
		var expectedOperator = mediaOperator.ToString().ToLower();
		var expectedMediaType = mediaType.ToString().ToLower();
		return $"{expectedOperator} {expectedMediaType}";
	}

	private static MediaQueryOperatorKeywordToken CreateMediaQueryOperator
	(
		MediaQueryOperatorKeyword operatorKeyword, string? text = null, ISourceElement? predecessor = null
	)
	{
		text = PrepareKeywordValue(operatorKeyword, text);
		var length = text.Length;
		var span = CreateSpan(predecessor, length);

		return new MediaQueryOperatorKeywordToken(operatorKeyword, text, span, Separator.Empty, Separator.Empty);
	}
}
