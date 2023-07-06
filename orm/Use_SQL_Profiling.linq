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


public class MyDbContext : DbContext
{
	// Your DbSet properties

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// Set up your database connection string
		optionsBuilder.UseSqlServer("YourConnectionString");

		// Enable SQL logging
		optionsBuilder.EnableSensitiveDataLogging(true);
	}
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			// Your database operations

			// Access the logged SQL statements
			var log = context.GetService<Microsoft.Extensions.Logging.ILoggerFactory>()
				.CreateLogger<MyDbContext>();
			var logs = log.LoggedSqlStatements;

			// Print the logged SQL statements
			foreach (var sql in logs)
			{
				Console.WriteLine(sql);
			}
		}
	}
}