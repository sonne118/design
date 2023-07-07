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

public async Task ProcessManyItems(List<string> items, int maxConcurrency = 10)
{
	using (var semaphore = new SemaphoreSlim(maxConcurrency))
	{
		var tasks = items.Select(async item =>
		{
			await semaphore.WaitAsync(); // Limit concurrency by waiting for the semaphore.
			try
			{
				await ProcessItem(item);
			}
			finally
			{
				semaphore.Release(); // Release the semaphore to allow other operations.
			}
		});

		await Task.WhenAll(tasks);
	}
}

private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
private readonly List<int> _list = new List<int>();

public async Task AddAsync(int item)
{
	await _semaphoreSlim.WaitAsync();
	try
	{
		_list.Add(item);
	}
	finally
	{
		_semaphoreSlim.Release();
	}
}