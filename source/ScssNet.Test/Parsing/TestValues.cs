namespace ScssNet.Test.Parsing;

public static class TestValues
{
	public static readonly string[] Units = ["1cm", "2mm", "2Q", "3in", "4pc", "5pt", "6px", "7em", "8rem", "9%"];

	public static readonly string[] IdentifierValues = ["red", "flex-start"];

	public static readonly string[] StringValues = [@"""Times New Roman""", @"""Courier New"""];

	public static readonly string[] HexColorValues = ["#123", "#abc", "#DEF", "#1a2", "#a1b", "#123456", "#abcdef", "#FEDCBA", "#1a2b3c", "#a1b2c3"];

	public static readonly string[] AllValues = [.. Units, .. IdentifierValues, .. StringValues, .. HexColorValues];

	public static readonly string[] OneOfEach = [Units[0], IdentifierValues[1], StringValues[0], HexColorValues[8]];
}
