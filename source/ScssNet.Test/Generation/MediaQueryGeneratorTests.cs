using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
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
		var mediaQuery = MediaQueryTypeKeywordToken.Create(mediaType, mediaTypeText);

		var provider = BuildServiceProvider();
		var mediaQueryGenerator = provider.GetRequiredService<MediaQueryGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		mediaQueryGenerator.Generate(mediaQuery, writer);

		var expected = GetExpected(mediaType);
		provider.GetStringWriter().ShouldContain(expected, StringCompareShould.IgnoreCase);
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
		var mediaQuery = MediaQueryUnaryExpression.Create(mediaOperator, mediaType, operatorText, typeText);

		var provider = BuildServiceProvider();
		var mediaQueryGenerator = provider.GetRequiredService<MediaQueryGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		mediaQueryGenerator.Generate(mediaQuery, writer);

		var expected = GetExpected(mediaOperator, mediaType);
		provider.GetStringWriter().ShouldContain(expected, StringCompareShould.IgnoreCase);
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
}
