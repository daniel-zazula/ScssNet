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
public class AtImportGeneratorTests: GeneratorTestBase
{
	public enum PathType { String, UrlFunction }

	[TestMethod]
	[DataRow(PathType.String)]
	[DataRow(PathType.UrlFunction)]
	public void ShouldGenerateFromAtRuleGenerator(PathType pathType)
	{
		var atImport = CreateAtImport(pathType);

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atImport, writer);

		AssertAtImport(provider, pathType);
	}

	[TestMethod]
	[DataRow(PathType.String)]
	[DataRow(PathType.UrlFunction)]
	public void ShouldGenerateFromAtImportGenerator(PathType pathType)
	{
		var atImport = CreateAtImport(pathType);

		var provider = BuildServiceProvider();
		var atImportGenerator = provider.GetRequiredService<AtImportGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atImportGenerator.Generate(atImport, writer);

		AssertAtImport(provider, pathType);
	}

	internal static AtImport CreateAtImport(PathType pathType = PathType.String)
	{
		var at = SymbolToken.Create(Symbol.At);
		var keyword = AtKeywordToken.Create(AtKeyword.Import, predecessor: at);

		IValue path = pathType switch
		{
			PathType.String => CreateStringPath(predecessor: keyword),
			PathType.UrlFunction => FunctionCall.Create("url", CreateStringPath),
			_ => throw InvalidPathTypeException(pathType)
		};

		var mediaQuery = MediaQueryTypeKeywordToken.Create(MediaQueryTypeKeyword.Screen, predecessor: path);
		var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: mediaQuery);

		return new AtImport(at, keyword, path, mediaQuery, semiColon);
	}

	internal static void AssertAtImport(ServiceProvider provider, PathType pathType = PathType.String)
	{
		var expected = pathType switch
		{
			PathType.String => "@import \"styles.css\" screen;",
			PathType.UrlFunction => "@import url(\"styles.css\") screen;",
			_ => throw InvalidPathTypeException(pathType)
		};

		provider.GetStringWriter().ShouldContain(expected, StringCompareShould.IgnoreCase);
	}

	private static StringToken CreateStringPath(ISourceElement? predecessor)
	{
		return StringToken.Create("\"styles.css\"", predecessor: predecessor);
	}

	private static ArgumentOutOfRangeException InvalidPathTypeException(PathType pathType)
	{
		return new ArgumentOutOfRangeException(nameof(pathType), pathType, "Invalid path type.");
	}
}
