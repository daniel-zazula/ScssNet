namespace ScssNet.Test.Parsing;

public static class TestSelectors
{
	public const string UniversalSelector = "*";
	public const string TagSelector = "div";
	public const string IdSelector = "#my-id";
	public const string ClassSelector = ".my-class";
	public const string PseudoClassSelector = ":hover";
	public const string PseudoElementSelector = "::before";

	public const string AttributeSelector = "[attr]";
	public const string AttributeValueSelector = "[attr=\"value\"]";
	public const string AttributeName = "attr";
	public const string AttributeValue = @"""value""";
}
