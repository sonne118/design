<Query Kind="Expression">
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

private static IDistributedCache _distributedCache;

public List<Product> GetPopularProducts()
{
	// Fetching popular product data from the distributed cache if available
	string cacheKey = "popularProducts";
	string cachedProducts = _distributedCache.GetString(cacheKey);
	if (cachedProducts == null)
	{
		var popularProducts = _dbContext.Products.Where(p => p.IsPopular).ToList();
		_distributedCache.SetString(cacheKey, JsonConvert.SerializeObject(popularProducts), new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
		});
		return popularProducts;
	}
	else
	{
		return JsonConvert.DeserializeObject<List<Product>>(cachedProducts);
	}
}