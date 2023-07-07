
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