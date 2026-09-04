using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
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
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateAtKeywordToken(AtKeyword.Import, predecessor: at);

		IValue path = pathType switch
		{
			PathType.String => CreateStringPath(predecessor: keyword),
			PathType.UrlFunction => CreateUrlFunctionCall(predecessor: keyword),
			_ => throw InvalidPathTypeException(pathType)
		};

		var semiColon = CreateSymbolToken(Symbol.SemiColon, predecessor: path);

		return new AtImport(at, keyword, path, semiColon);
	}

	internal static void AssertAtImport(ServiceProvider provider, PathType pathType = PathType.String)
	{
		var expected = pathType switch
		{
			PathType.String => "@import \"styles.css\";",
			PathType.UrlFunction => "@import url(\"styles.css\");",
			_ => throw InvalidPathTypeException(pathType)
		};

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}

	private static StringToken CreateStringPath(ISourceElement? predecessor)
	{
		return CreateStringToken("\"styles.css\"", predecessor: predecessor);
	}

	private static FunctionCall CreateUrlFunctionCall(ISourceElement? predecessor)
	{
		var name = CreateIdentifierToken("url", predecessor: predecessor);
		var openParenthesis = CreateSymbolToken(Symbol.OpenParenthesis, predecessor: name);
		var path = CreateStringPath(predecessor: openParenthesis);
		var closeParenthesis = CreateSymbolToken(Symbol.CloseParenthesis, predecessor: path);

		return new FunctionCall(name, openParenthesis, path, closeParenthesis);
	}

	private static ArgumentOutOfRangeException InvalidPathTypeException(PathType pathType)
	{
		return new ArgumentOutOfRangeException(nameof(pathType), pathType, "Invalid path type.");
	}
}
