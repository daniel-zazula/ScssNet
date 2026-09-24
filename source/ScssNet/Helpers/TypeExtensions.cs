namespace ScssNet;

public static class TypeExtensions
{
	public static bool IsSubclassOfGeneric(this Type type, Type generic)
	{
		while(type != null && type != typeof(object))
		{
			if(type.IsGenericType && type.GetGenericTypeDefinition() == generic)
				return true;

			type = type.BaseType;
		}
		return false;
	}
}
