namespace ScssNet.Generation;

internal interface IGenerator<T> where T: ISourceElement
{
	void Generate(T element, CssWriter writer);
}
