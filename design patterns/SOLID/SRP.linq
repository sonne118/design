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

Console.WriteLine("*** A demo that follows SRP.***");

Employee robin = new("Robin", "Smith", 7.5);

Helper.PrintEmployeeDetail(robin);
Helper.PrintEmployeeId(robin);
Helper.PrintSeniorityLevel(robin);

Console.WriteLine("*******");

Employee kevin = new("Kevin", "Proctor", 3.2);

Helper.PrintEmployeeDetail(kevin);
Helper.PrintEmployeeId(kevin);
Helper.PrintSeniorityLevel(kevin);

class Employee
{
	public string FirstName, LastName;
	public double ExperienceInYears;
	public Employee(
		string firstName,
		string lastName,
		double experience)
	{
		FirstName = firstName;
		LastName = lastName;
		ExperienceInYears = experience;
	}

	public void DisplayEmployeeDetail()
	{
		Console.WriteLine($"The employee name: {LastName}, {FirstName}");
		Console.WriteLine($"This employee has {ExperienceInYears} years of experience.");
	}
}
class SeniorityChecker
{
	public string CheckSeniority(double experienceInYears)
	{
		if (experienceInYears > 5)
			return "senior";
		else
			return "junior";
	}

}
class EmployeeIdGenerator
{
	public string Id = "Not generated yet";
	public string GenerateEmployeeId(string empFirstName)
	{
		int random = new Random().Next(1000);
		Id = string.Concat(empFirstName[0], random);
		return Id;
	}
}
class Helper
{
	public static void PrintEmployeeDetail(Employee emp)
	{
		emp.DisplayEmployeeDetail();
	}

	public static void PrintEmployeeId(Employee emp)
	{
		EmployeeIdGenerator idGenerator = new();
		string empId = idGenerator.GenerateEmployeeId(emp.FirstName);
		Console.WriteLine($"The employee id: {empId}");
	}
	public static void PrintSeniorityLevel(Employee emp)
	{
		SeniorityChecker seniorityChecker = new();
		string seniorityLevel = seniorityChecker.CheckSeniority(emp.ExperienceInYears);
		Console.WriteLine($"This employee is a {seniorityLevel} employee.");
	}
}



//class Employee
//{
//	public string FirstName, LastName;
//	public string Id;
//	public double ExperienceInYears;
//	public Employee(
//		string firstName,
//		string lastName,
//		double experience)
//	{
//		FirstName = firstName;
//		LastName = lastName;
//		ExperienceInYears = experience;
//		Id = "Not generated yet";
//	}
//	public void DisplayEmployeeDetail()
//	{
//		Console.WriteLine($"The employee name: {LastName}, {FirstName}");
//		Console.WriteLine($"This employee has {ExperienceInYears} years of experience.");
//	}
//
//	public string CheckSeniority(double experienceInYears)
//	{
//		if (experienceInYears > 5)
//			return "senior";
//		else
//			return "junior";
//	}
//	public string GenerateEmployeeId(string empFirstName)
//	{
//		int random = new Random().Next(1000);
//		Id = string.Concat(empFirstName[0], random);
//		return Id;
//	}
//}

