public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
{
	using (var cts = new CancellationTokenSource())
	{
		var delayTask = Task.Delay(timeout, cts.Token);

		var resultTask = await Task.WhenAny(task, delayTask);
		if (resultTask == delayTask)
		{
			// Operation cancelled
			throw new OperationCanceledException();
		}
		else
		{
			// Cancel the timer task so that it does not fire
			cts.Cancel();
		}

		return await task;
	}
}