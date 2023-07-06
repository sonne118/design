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
  <Namespace>Reusables.Storage.Entities</Namespace>
  <Namespace>Reusables.Utils</Namespace>
  <Namespace>StackExchange.Redis</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public interface IProductsCache
{
	ValueTask<Product> TryGet(long productId);
	ValueTask Set(long productId, Product value);
}

namespace Reusables.Caching.Redis
{
	public class ProductsRedisCache : IProductsCache
	{
		private static readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

		private readonly ISerializer _serializer;
		private readonly IRedisManager _redisManager;

		public ProductsRedisCache(
			ISerializer serializer,
			IRedisManager redisManager
			)
		{
			_serializer = serializer;
			_redisManager = redisManager;
		}

		public async ValueTask Set(long productId, Product value)
		{
			var key = GetKey(productId);
			var valueSerialized = _serializer.Serialize(value);

			await _redisManager.SetValue(key, valueSerialized, TTL);
			ProductsRedisCacheEventSource.Log.ItemAdded();
		}

		public async ValueTask<Product> TryGet(long productId)
		{
			ProductsRedisCacheEventSource.Log.ItemRequested();

			var redisValue = await _redisManager.GetValue(GetKey(productId));
			if (!redisValue.HasValue)
			{
				ProductsRedisCacheEventSource.Log.ItemNotFound();
				return null;
			}

			var valueDeserialized = _serializer.Deserialize<Product>(redisValue);
			ProductsRedisCacheEventSource.Log.ItemFetched();
			return valueDeserialized;
		}

		private static RedisKey GetKey(long productId) => $"Product-{productId}";
	}
}
