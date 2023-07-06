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

public class YourDbContext : DbContext
{
	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		// Configure indexes on specific properties or columns
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.SomeProperty)
			.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

		// Alternatively, you can use fluent API to define indexes
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.SomeProperty)
			.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));

		// Configure composite indexes on multiple properties or columns
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.Property1)
			.HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute(), new IndexAttribute() }));
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.Property2)
			.HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute(), new IndexAttribute() }));
	}
}