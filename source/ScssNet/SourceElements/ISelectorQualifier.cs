namespace ScssNet.SourceElements;

public interface ISelectorQualifier : ISelector
{
	ISelectorQualifier? Qualifier { get; }
}
