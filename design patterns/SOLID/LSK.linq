<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>App.Metrics.AspNetCore</NuGetReference>
  <NuGetReference>App.Metrics.AspNetCore.Core</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http.Features</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Mvc</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Configuration</NuGetReference>
  <NuGetReference>Microsoft.Extensions.DependencyInjection</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Http.Polly</NuGetReference>
  <NuGetReference>NUnitLite</NuGetReference>
  <NuGetReference>System.Threading.Tasks.Dataflow</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

Console.WriteLine("***A demo that follows LSP.***");
PaymentHelper helper = new();

// Instantiating two registered users.
RegisteredUser robin = new("Robin");
RegisteredUser jack = new("Jack");

// Adding the users to the helper.
helper.AddPreviousPayment(robin);
helper.AddPreviousPayment(jack);
helper.AddNewPayment(robin);
helper.AddNewPayment(jack);

// Instantiating a guest user.
GuestUser guestUser1 = new();
helper.AddNewPayment(guestUser1);

// Retrieve all the previous payments
// of registered users.
helper.ShowPreviousPayments();

// Process all new payment requests
// from all users.
helper.ProcessNewPayments();


interface IPreviousPayment
{
	void LoadPreviousPaymentInfo();
}
interface INewPayment
{
	void ProcessNewPayment();
}
class RegisteredUser : IPreviousPayment, INewPayment
{
	readonly string name = string.Empty;
	public RegisteredUser(string name)
	{
		this.name = name;
	}
	public void LoadPreviousPaymentInfo()
	{
		Console.WriteLine($"Retrieving {name}'s last payment details.");
	}

	public void ProcessNewPayment()
	{
		Console.WriteLine($"Processing {name}'s current payment request0.");
	}
}

class GuestUser : INewPayment
{
	readonly string name = string.Empty;
	public GuestUser()
	{
		this.name = "guest user";
	}

	public void ProcessNewPayment()
	{
		Console.WriteLine($"Processing a {name}'s current payment request1.");
	}
}

class PaymentHelper
{
	readonly List<IPreviousPayment> previousPayments = new();
	readonly List<INewPayment> newPaymentRequests = new();
	public void AddPreviousPayment(IPreviousPayment previousPayment)
	{
		previousPayments.Add(previousPayment);
	}

	public void AddNewPayment(INewPayment newPaymentRequest)
	{
		newPaymentRequests.Add(newPaymentRequest);
	}
	public void ShowPreviousPayments()
	{
		foreach (IPreviousPayment user in previousPayments)
		{
			user.LoadPreviousPaymentInfo();			
			Console.WriteLine("------");
		}
	}
	public void ProcessNewPayments()
	{
		foreach (INewPayment payment in newPaymentRequests)
		{
			payment.ProcessNewPayment();
			Console.WriteLine("***********");
		}
	}
}



//interface IPayment
//{
//	void LoadPreviousPaymentInfo();
//	void ProcessNewPayment();
//}
//class RegisteredUser : IPayment
//{
//	readonly string name = string.Empty;
//	public RegisteredUser(string name)
//	{
//		this.name = name;
//	}
//	public void LoadPreviousPaymentInfo()
//	{
//		Console.WriteLine($"Retrieving {name}'s last payment details.");
//	}
//
//	public void ProcessNewPayment()
//	{
//		Console.WriteLine($"Processing {name}'s current payment request.");
//	}
//}
//
//class GuestUser : IPayment
//{
//	readonly string name = string.Empty;
//	public GuestUser()
//	{
//		name = "guest user";
//	}
//
//	public void LoadPreviousPaymentInfo()
//	{
//		throw new NotImplementedException();
//	}
//
//	public void ProcessNewPayment()
//	{
//		Console.WriteLine($"Processing {name}'s current payment request.");
//	}
//}
//
//class PaymentHelper
//{
//	readonly List<IPayment> users = new();
//	public void AddUser(IPayment user)
//	{
//		users.Add(user);
//	}
//	public void ShowPreviousPayments()
//	{
//		foreach (IPayment user in users)
//		{
//			user.LoadPreviousPaymentInfo();
//			Console.WriteLine("------");
//		}
//	}
//	public void ProcessNewPayments()
//	{
//		foreach (IPayment user in users)
//		{
//			user.ProcessNewPayment();
//			Console.WriteLine("***********");
//		}
//	}
//}





