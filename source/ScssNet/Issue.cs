namespace ScssNet;

public enum IssueType
{
	Error, Warning, Notice
}

public record Issue
{
	public IssueType Type { get; }
	public string Message { get; }

	public Issue(IssueType type, string message)
	{
		Type = type;
		Message = message;
	}
}
