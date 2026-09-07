using System;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class MediaParserTests : ParserTestBase
{
	[TestMethod]
	[DynamicData(nameof(GetMediaTypesData))]
	public void ShouldParseMediaQueryType(MediaQueryTypeKeyword mediaType)
	{
		var source = GetSource(mediaType);

		var provider = BuildServiceProvider(source);
		
		var tokenReader = provider.GetRequiredService<TokenReader>();
		var mediaParser = provider.GetRequiredService<MediaQueryParser>();

		var mediaQuery = mediaParser.Parse(tokenReader);
		mediaQuery.ShouldNotBeNull();
		mediaQuery.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		AssertMediaType(mediaQuery, mediaType);
	}

	[TestMethod]
	[DynamicData(nameof(GetUnaryExpressionData))]
	public void ShouldParseMediaQueryUnaryExpression
	(
		MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType
	)
	{
		var source = GetSource(mediaOperator, mediaType);

		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<TokenReader>();
		var mediaParser = provider.GetRequiredService<MediaQueryParser>();

		var mediaQuery = mediaParser.Parse(tokenReader);
		mediaQuery.ShouldNotBeNull();
		mediaQuery.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		AssertMediaQueryExpression(mediaQuery, mediaOperator, mediaType);
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

	internal static string GetSource<T>(T enumItem) where T: Enum
	{
		return enumItem.ToString().ToLowerInvariant();
	}

	internal static string GetSource(MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType)
	{
		return $"{GetSource(mediaOperator)} {GetSource(mediaType)}";
	}

	internal static void AssertMediaQueryExpression
	(
		IMediaQuery mediaQuery, MediaQueryOperatorKeyword mediaOperator, MediaQueryTypeKeyword mediaType
	)
	{
		var mediaQueryUnaryExpression = mediaQuery.ShouldBeOfType<MediaQueryUnaryExpression>();
		mediaQueryUnaryExpression.OperatorKeyword.Keyword.ShouldBe(mediaOperator);
		mediaQueryUnaryExpression.OperatorKeyword.Text.ShouldBe(GetSource(mediaOperator));
		AssertMediaType(mediaQueryUnaryExpression.Value, mediaType);
	}

	internal static void AssertMediaType(IMediaQuery mediaQuery, MediaQueryTypeKeyword mediaType)
	{
		var mediaQueryTypeToken = mediaQuery.ShouldBeOfType<MediaQueryTypeKeywordToken>();
		mediaQueryTypeToken.Keyword.ShouldBe(mediaType);
		mediaQueryTypeToken.Text.ShouldBe(GetSource(mediaType));
	}
}
