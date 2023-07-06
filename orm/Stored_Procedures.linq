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

using (var context = new YourDbContext())
{
	// Call a stored procedure using DbContext.Database.ExecuteSqlCommand
	int rowsAffected = context.Database.ExecuteSqlCommand("EXEC YourStoredProcedure @Parameter1, @Parameter2",
		new SqlParameter("@Parameter1", value1),
		new SqlParameter("@Parameter2", value2));

	// Execute a stored procedure and map the result to entities
	var results = context.YourEntities
		.FromSql("EXEC YourStoredProcedure @Parameter1, @Parameter2",
			new SqlParameter("@Parameter1", value1),
			new SqlParameter("@Parameter2", value2))
		.ToList();

	// Access the results
	// ...
}