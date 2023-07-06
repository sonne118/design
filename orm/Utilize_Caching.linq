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
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.Extensions.Caching.Memory</Namespace>
</Query>

public class MyDbContext : DbContext
{
	public MyDbContext()
		: base("YourConnectionString")
	{
		this.Configuration.LazyLoadingEnabled = false;
		this.Configuration.ProxyCreationEnabled = false;
	}

	public DbSet<YourEntity> YourEntities { get; set; }

	public override int SaveChanges()
	{
		ClearCache(); // Clear the cache before saving changes
		return base.SaveChanges();
	}

	private void ClearCache()
	{
		ObjectCache cache = MemoryCache.Default;
		foreach (var item in cache)
		{
			if (item.Key.ToString().StartsWith("YourEntities"))
				cache.Remove(item.Key);
		}
	}

	public List<YourEntity> GetEntitiesFromCache()
	{
		ObjectCache cache = MemoryCache.Default;
		var entities = cache["YourEntities"] as List<YourEntity>;

		if (entities == null)
		{
			entities = this.YourEntities.ToList();
			CacheItemPolicy policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10);
			cache.Set("YourEntities", entities, policy);
		}

		return entities;
	}
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			var entities = context.GetEntitiesFromCache();

			// Perform your operations on entities

			// Save changes
			context.SaveChanges();
		}
	}
}