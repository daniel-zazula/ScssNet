using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class IdSelectorCreation
{
	extension(IdSelector)
	{
		internal static IdSelector Create(ISourceElement? predecessor = null)
		{
			var hash = HashValueToken.Create("#myid", predecessor: predecessor);
			return new IdSelector(hash, null);
		}
	}
}
