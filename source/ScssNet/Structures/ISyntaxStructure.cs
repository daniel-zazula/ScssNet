namespace ScssNet.Structures;

public interface ISyntaxStructure : ISourceElement
{
}

public interface IStatement : ISyntaxStructure
{
}

public interface INestableStatement : IStatement
{
}
