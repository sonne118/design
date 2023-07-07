
public void GetAwaiterGetResultExample()
{
	//This is ok, but if an error is thrown, it will be encapsulated in an AggregateException   
	string data = GetData().Result;

	//This is better, if an error is thrown, it will be contained in a regular Exception
	data = GetData().GetAwaiter().GetResult();
}