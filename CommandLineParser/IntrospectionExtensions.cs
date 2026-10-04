namespace CommandLine
{
#if NET40
 
	public static class IntrospectionExtensions
	{
		public static Type GetTypeInfo(this Type type)
		{
			return type;
		}
	}
#endif
}
