public abstract  class Template
{

    delegate bool HandlerDelegate(string str);
	
    public abstract bool Process(string str);


	private static HandlerDelegate Creator()
	{
		var some1 = new SomeClass1();
		var some2 = new SomeClass2();
		var some3 = new SomeClass3();

		var manager = new HandlerDelegate(some1.Process);
		manager += new HandlerDelegate(some2.Process);
		manager += new HandlerDelegate(some3.Process);
		return manager;
	}

	public static void Execute()
	{
		HandlerDelegate manager = Creator();
		
		string str = "some string pass";
	    manager(str);
		
	}
}

public class SomeClass1 : Template
{
	
	public override bool Process(string str)
	{
	  // TODO something
	  return true;
	}

}

public class SomeClass2 : Template
{

	public override bool Process(string str)
	{
		// TODO something
		return true;
	}
}

public class SomeClass3 : Template
{

	public override bool Process(string str)
	{
		// TODO something
		return true;
	}
}

