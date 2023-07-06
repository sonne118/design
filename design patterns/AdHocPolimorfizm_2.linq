
void Main()
{
	 Base o = new Derived2();
	 o.Foo();	 
	 ((Derived)o).Foo();
	 ((Derived2)o).Foo();

	Colleague obj = new CustomerColleague();
	obj.Send("Pass to send");
	
	obj.Notify("???.class");
	
	((CustomerColleague)obj).Send("Pass to send");
	
	obj.Notify("Derived.class");

}

public class Base
{

	public virtual void Foo()
	{
		Console.WriteLine("Foo.Base");
	}
}

public class Derived : Base
{

	public override void Foo()
	{
		Console.WriteLine("Foo.Derived");
	}
}

public class Derived2 : Derived
{

	public new void Foo()
	{
		Console.WriteLine("Foo.Derived2");
	}
}

public abstract class Colleague
{
	public  void Send(string message)
	{
		Console.WriteLine("{0}-----{1}", message, "abstract");
	}
	public abstract void Notify(string message);
}

public class CustomerColleague : Colleague
{

	public new  void Send(string message)
	{
		Console.WriteLine("{0}-----{1}", message, "derived");
	}
	
	public override void Notify(string message)
	{
		Send("Pass to send");
		Console.WriteLine(message);
	}
}
