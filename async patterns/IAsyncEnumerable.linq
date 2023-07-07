
public async IAsyncEnumerable<int> FetchItemsAsync()
{
	for (int i = 0; i < 10; i++)
	{
		// Simulating async operation to retrieve data
		await Task.Delay(100);
		yield return i;
	}
}

public async Task ConsumeItemsAsync()
{
	await foreach (var item in FetchItemsAsync())
	{
		Console.WriteLine(item);
	}
}