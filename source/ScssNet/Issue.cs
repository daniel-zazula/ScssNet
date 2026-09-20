global using Issues = System.Collections.Generic.IEnumerable<ScssNet.Issue>;

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

internal static class IssueExtensions
{
	extension(Issues)
	{
		internal static Issues ConcatFrom(params ISourceElement?[] sourceElements)
		{
			return ConcatFrom((IEnumerable<ISourceElement?>)sourceElements);
		}

		internal static Issues ConcatFrom(IEnumerable<ISourceElement?> sourceElements)
		{
			return sourceElements.SelectMany(se => se?.Issues ?? []);
		}
	}
}
