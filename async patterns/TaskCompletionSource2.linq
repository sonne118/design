
public Task<int> DoSomethingAsync()
{
	var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

	var operation = new LegacyAsyncOperation();
	operation.Completed += result =>
	{
		// Code awaiting on this task will resume on a different thread-pool thread
		tcs.SetResult(result);
	};

	return tcs.Task;
}