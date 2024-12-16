using ScssNet.Parsing;

namespace ScssNet.Lexing
{
	public interface IToken: ISourceElement { }

	public interface IValueToken: IValue, IToken { }
}
