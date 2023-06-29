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

Console.WriteLine("***A demo that follows ISP.***");

IPrinter printer = new BasicPrinter();
printer.PrintDocument();
printer = new AdvancedPrinter();
printer.PrintDocument();

IFaxDevice faxDevice = new AdvancedPrinter();
faxDevice.SendFax();

interface IPrinter
{
	void PrintDocument();
}
interface IFaxDevice
{
	void SendFax();
}
class BasicPrinter : IPrinter
{
	public void PrintDocument()
	{
		Console.WriteLine("A basic printer prints the document.");
	}
}
class AdvancedPrinter : IPrinter, IFaxDevice
{
	public void PrintDocument()
	{
		Console.WriteLine("An advanced printer prints the document.");
	}
	public void SendFax()
	{
		Console.WriteLine("An advanced printer sends the fax.");
	}
}




//List<IPrinter> printers = new()
//{
// new AdvancedPrinter(),
// new BasicPrinter()
// };
//foreach (IPrinter device in printers)
//{
//    device.PrintDocument();
//    device.SendFax(); // Will throw exception
//}


//interface IPrinter
//{
//	void PrintDocument();
//	void SendFax();
//}
//
//
//class AdvancedPrinter : IPrinter
//{
//	public void PrintDocument()
//	{
//		Console.WriteLine("An advanced printer prints the document.");
//	}
//	public void SendFax()
//	{
//		Console.WriteLine("An advanced printer sends the fax.");
//	}
//}
//class BasicPrinter : IPrinter
//{
//	public void PrintDocument()
//	{
//		Console.WriteLine("A basic printer prints the document.");
//	}
//
//	public void SendFax()
//	{
//		throw new NotImplementedException();
//	}
//}





