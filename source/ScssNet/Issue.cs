namespace ScssNet
{
	public enum IssueType
	{
		Error, Warning, Notice
	}

	public class Issue(IssueType type, string message)
	{
		public IssueType Type => type;
		public string Message => message;
	}
}
