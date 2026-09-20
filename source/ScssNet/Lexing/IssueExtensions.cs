namespace ScssNet.Lexing;

internal static class IssueExtensions
{
	extension(Issue)
	{
		public static Issue CreateExpected(string expected)
		{
			return new Issue(IssueType.Error, "Expected " + expected);
		}

		public static Issue CreateExpected<T>() where T: Enum
		{
			var keywords = (T[])Enum.GetValues(typeof(T));
			var expected = "one of the keywords: " + string.Join(", ", keywords);
			return CreateExpected(expected);
		}
	}
}
