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

	internal static AtImport CreateAtImport(PathType pathType)
	{
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateKeywordToken(Keyword.Import, columnNumber: at.End.ColumnNumber + 1);

		var pathColumnNumber = keyword.End.ColumnNumber + 1;
		IValue path = pathType switch
		{
			PathType.String => CreateStringToken("\"styles.css\"", columnNumber: pathColumnNumber),
			PathType.UrlFunction => CreateUrlFunctionCall(pathColumnNumber),
			_ => throw InvalidPathTypeException(pathType)
		};

		var semiColon = CreateSymbolToken(Symbol.SemiColon, columnNumber: path.End.ColumnNumber + 1);

		return new AtImport(at, keyword, path, semiColon);
	}

	internal static void AssertAtImport(ServiceProvider provider, PathType pathType)
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

	private static StringToken CreateStringPath(int columnNumber)
	{
		return CreateStringToken("\"styles.css\"", columnNumber: columnNumber);
	}

	private static FunctionCall CreateUrlFunctionCall(int columnNumber)
	{
		var name = CreateIdentifierToken("url", columnNumber);
		var openParenthesis = CreateSymbolToken(Symbol.OpenParenthesis, columnNumber: name.End.ColumnNumber + 1);
		var path = CreateStringPath(openParenthesis.End.ColumnNumber + 1);
		var closeParenthesis = CreateSymbolToken(Symbol.CloseParenthesis, columnNumber: path.End.ColumnNumber + 1);

		return new FunctionCall(name, openParenthesis, path, closeParenthesis);
	}

	private static ArgumentOutOfRangeException InvalidPathTypeException(PathType pathType)
	{
		return new ArgumentOutOfRangeException(nameof(pathType), pathType, "Invalid path type.");
	}
}
