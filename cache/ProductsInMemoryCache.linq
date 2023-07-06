<Query Kind="Program">
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
  <Namespace>Reusables.Storage.Entities</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
}

namespace Reusables.Caching.InMemory
{
	public class ProductsInMemoryCache : IProductsCache
	{
		private const int Capacity = 5_000;
		//private const int Capacity = 13_000;
		//private const int Capacity = 15_000;
		private readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

		private readonly LRUCache<long, Product> _products;
		public ProductsInMemoryCache()
		{
			_products = new LRUCache<long, Product>(Capacity, TTL);
		}

		private static readonly ValueTask<Product> NullProductValueTask = ValueTask.FromResult((Product)null);
		public ValueTask<Product> TryGet(long key)
		{
			if (_products.TryGet(key, out var value))
				return ValueTask.FromResult(value);
			return NullProductValueTask;
		}

		ValueTask IProductsCache.Set(long key, Product value)
		{
			_products.Set(key, value);
			return ValueTask.CompletedTask;
		}
	}
}
