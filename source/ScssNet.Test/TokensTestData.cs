using System;
using ScssNet.Tokens;

namespace ScssNet.Test;

internal static class TokensTestData
{
	internal static readonly string[] Comments = ["//line comment\r\n", "/*two line\r\ncomment*/"];

	internal static readonly string[] HexColors =
	[
		"#123", "#def", "#dEf", "#DEF", "#1b3", "#1B3", "#a2C", "#a2C", "#A2c", "#A2C",
		"#654321", "#fedcba", "#FeDcBa", "#FEDCBA", "#6e4c2a", "#6E4c2A", "#6E4C2A", "#f5d3b1", "#f5D3b1", "#F5D3B1"
	];

	internal static readonly string[] IdSelectors =
	[
		"#four", "#with-dash", "#with_underscore", "#with123numbers", "#with-dash-and_underscore-123"
	];

	internal static IEnumerable<string> HashValues => HexColors.Concat(IdSelectors);

	internal static readonly string[] Identifiers =
	[
		"table", "CamelCase", "h4", "custom-class", "-experimental-property", "--custom-var"
	];

	internal static readonly string[] Strings = ["\"Some string\"", "'Other string'"];

	internal static readonly Symbol[] Symbols = Enum.GetValues<Symbol>();

	internal static IEnumerable<string> SymbolStrings => Symbols.Select(s => s.ToChars());

	internal static IEnumerable<(string value, string unit)> UnitValues =
	[
		("3", ""), ("10", "px"), ("50", "%"), ("-10.2", "cm"), ("-200", "mm"), ("2", "Q"), ("-3.5", "in"), ("10", "pt"),
		("5", "pc")
	];

	internal static IEnumerable<string> UnitValueStrings => UnitValues.Select(u => $"{u.value}{u.unit}");

	internal static IEnumerable<string> AllTokens = Comments.Concat(HashValues).Concat(Identifiers).Concat(Strings)
		.Concat(SymbolStrings).Concat(UnitValueStrings);

	internal static IEnumerable<string> OneOfEach => HashValues.Take(1).Concat(Identifiers.Take(1))
		.Concat(Strings.Take(1)).Concat(SymbolStrings.Take(1)).Concat(UnitValueStrings.Take(1));
}
