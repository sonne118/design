

public async Task PerformLongRunningOperationAsync()
{
	for (int i = 0; i < 100; i++)
	{
		// Perform a partial computation
		DoPartialComputation();

		// Yield control back to the calling context (e.g., the UI thread)
		await Task.Yield();
	}
}

public async Task PerformLongRunningOperationAsync()
{
	for (int i = 0; i < 100; i++)
	{
		// Perform a partial computation that requires shared resources
		DoPartialComputationWithSharedResource();

		// Yield control back to the calling context to allow other tasks to access shared resources
		await Task.Yield();
	}
}