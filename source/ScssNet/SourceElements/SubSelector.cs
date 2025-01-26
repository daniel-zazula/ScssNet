namespace ScssNet.SourceElements
{
	public class SubSelector(ISelector parentSelector, ISelector childSelector) : ISourceElement
	{
		public ISelector ParentSelector => parentSelector;
		public ISelector ChildSelector => childSelector;

		public IEnumerable<Issue> Issues => SourceElement.List(parentSelector, childSelector).ConcatIssues();

		public SourceCoordinates Start => parentSelector.Start;

		public SourceCoordinates End => childSelector.End;
	}
}
