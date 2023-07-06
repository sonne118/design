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

	public IEnumerable<TEntity> GetEntitiesWithPagination<TEntity>(int pageNumber, int pageSize)
		where TEntity : class
	{
		return Set<TEntity>()
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToList();
	}
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			// Get entities with pagination
			int pageNumber = 1;
			int pageSize = 10;
			var entities = context.GetEntitiesWithPagination<TEntity>(pageNumber, pageSize);

			// Process the entities
			foreach (var entity in entities)
			{
				// Do something with the entity
			}
		}
	}
}