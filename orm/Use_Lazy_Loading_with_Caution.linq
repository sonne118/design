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
	// Disable lazy loading explicitly
	context.Configuration.LazyLoadingEnabled = false;

	// Load related entities explicitly using Include method
	var entity = context.YourEntities
		.Include(e => e.RelatedEntity)
		.FirstOrDefault();

	// Access the related entities
	// ...

	// Explicitly load related entities using Load method
	context.Entry(entity)
		.Reference(e => e.RelatedEntity)
		.Load();

	// Access the loaded related entity
	// ...

	// Avoid accessing navigation properties outside the context scope
	// ...

	// Consider using eager loading or explicit loading instead of lazy loading
	// ...

	// Dispose the context when you're done
	// ...
}