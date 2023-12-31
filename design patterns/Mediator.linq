
public abstract class Mediator
{
	public abstract void Send(string msg, Colleague colleague);
}

public abstract class Colleague
{
	private Mediator mediator;

	public Colleague(Mediator mediator)
	{
		this.mediator = mediator;
	}

	public virtual void Send(string message)
	{
		mediator.Send(message, this);
	}
	public abstract void Notify(string message);
}
// класс заказчика
public class CustomerColleague : Colleague
{
	public CustomerColleague(Mediator mediator)
		: base(mediator)
	{ }

	public override void Notify(string message)
	{
		Console.WriteLine("Сообщение заказчику: " + message);
	}
}
// класс программиста
public class ProgrammerColleague : Colleague
{
	public ProgrammerColleague(Mediator mediator)
		: base(mediator)
	{ }

	public override void Notify(string message)
	{
		Console.WriteLine("Сообщение программисту: " + message);
	}
}
// класс тестера
public class TesterColleague : Colleague
{
	public TesterColleague(Mediator mediator)
		: base(mediator)
	{ }

	public override void Notify(string message)
	{
		Console.WriteLine("Сообщение тестеру: " + message);
	}
}

public class ManagerMediator : Mediator
{
	public Colleague Customer { get; set; }
	public Colleague Programmer { get; set; }
	public Colleague Tester { get; set; }
	public override void Send(string msg, Colleague colleague)
	{		
		if (Customer == colleague)
			Programmer.Notify(msg);		
		else if (Programmer == colleague)
			Tester.Notify(msg);		
		else if (Tester == colleague)
			Customer.Notify(msg);
	}
}