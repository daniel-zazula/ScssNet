namespace ScssNet.SourceElements
{
	public interface ICompoundSelector : ISourceElement
	{
		ICompoundSelector? Qualifier { get; }
	}
}
