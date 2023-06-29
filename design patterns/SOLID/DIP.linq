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

Console.WriteLine("***A demo that follows DIP.***");

// Using Oracle now
IDatabase database = new OracleDatabase();
UserInterface userInterface = new(database);
userInterface.SaveEmployeeId("E001");

// Using MySQL now
database = new MySQLDatabase();
userInterface = new UserInterface(database);
userInterface.SaveEmployeeId("E002");
class UserInterface
{
	readonly IDatabase database;
	public UserInterface(IDatabase database)
	{
		this.database = database;
	}
	public void SaveEmployeeId(string empId)
	{
		database.SaveEmpIdInDatabase(empId);
	}

}
interface IDatabase
{
	void SaveEmpIdInDatabase(string empId);
}
class OracleDatabase : IDatabase
{
	public void SaveEmpIdInDatabase(string empId)
	{
		Console.WriteLine($"The id: {empId} is saved in the Oracle database.");
	}
}

class MySQLDatabase : IDatabase
{
	public void SaveEmpIdInDatabase(string empId)
	{
		Console.WriteLine($"The id: {empId} is saved in the MySQL database.");
	}
}



