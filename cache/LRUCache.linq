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
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
}

namespace Reusables.Caching.InMemory
{
	public sealed class LRUCache<TKey, TValue>
	{
		/// <summary>
		/// Default maximum number of elements to cache.
		/// </summary>
		private const int DefaultCapacity = 255;
		private static readonly TimeSpan DefaultTTL = TimeSpan.FromMinutes(2);


		private readonly object _lockObj = new object();
		private readonly int _capacity;
		private readonly TimeSpan _ttl;
		private readonly Dictionary<TKey, Entry> _cacheMap;
		private readonly LinkedList<TKey> _cacheList;

		/// <summary>
		/// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class.
		/// </summary>
		public LRUCache()
			: this(DefaultCapacity, DefaultTTL)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class.
		/// </summary>
		/// <param name="capacity">Maximum number of elements to cache.</param>
		public LRUCache(int capacity, TimeSpan ttl)
		{
			_capacity = capacity > 0 ? capacity : DefaultCapacity;
			_ttl = ttl;
			_cacheMap = new Dictionary<TKey, Entry>(_capacity);
			_cacheList = new LinkedList<TKey>();
		}

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with
		/// the specified key, if the key is found; otherwise, the default value for the 
		/// type of the <paramref name="value" /> parameter.</param>
		/// <returns>true if contains an element with the specified key; otherwise, false.</returns>
		public bool TryGet(TKey key, out TValue value)
		{
			LRUCacheEventSource.Log.ItemRequested();

			lock (_lockObj)
			{
				if (_cacheMap.TryGetValue(key, out var entry))
				{
					if ((DateTime.Now - entry.DateAdded).TotalMilliseconds > _ttl.TotalMilliseconds)
					{
						_cacheList.Remove(entry.Node);
						_cacheMap.Remove(key);
						LRUCacheEventSource.Log.ItemExpired();

						value = default;
						return false;
					}

					Touch(entry.Node);
					value = entry.Value;
					LRUCacheEventSource.Log.ItemFetched();
					return true;
				}
			}

			value = default;
			LRUCacheEventSource.Log.ItemNotFound();
			return false;
		}

		/// <summary>
		/// Adds the specified key and value to the cache.
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		public void Set(TKey key, TValue value)
		{
			lock (_lockObj)
			{
				if (!_cacheMap.TryGetValue(key, out var entry))
				{
					LinkedListNode<TKey> node;
					if (_cacheMap.Count >= _capacity)
					{
						node = _cacheList.Last;
						_cacheMap.Remove(node.Value);
						_cacheList.RemoveLast();
						node.Value = key;
					}
					else
					{
						node = new LinkedListNode<TKey>(key);
					}

					_cacheList.AddFirst(node);
					_cacheMap.Add(key, new Entry(node, value, DateTime.Now));
					LRUCacheEventSource.Log.ItemAdded();
				}
				else
				{
					entry.Value = value;
					_cacheMap[key] = entry;
					Touch(entry.Node);
				}
			}
		}

		private void Touch(LinkedListNode<TKey> node)
		{
			if (node != _cacheList.First)
			{
				_cacheList.Remove(node);
				_cacheList.AddFirst(node);
			}
		}

		private struct Entry
		{
			public LinkedListNode<TKey> Node;
			public TValue Value;
			public DateTime DateAdded;

			public Entry(LinkedListNode<TKey> node, TValue value, DateTime dateAdded)
			{
				Node = node;
				Value = value;
				DateAdded = dateAdded;
			}
		}
	}
}
