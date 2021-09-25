public static class Swap
{
	public static void S<T>(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}
}