public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
{
	var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

	// This disposes the registration as soon as one of the tasks trigger
	using (cancellationToken.Register(state =>
	{
		((TaskCompletionSource<object>)state).TrySetResult(null);
	},
	tcs))
	{
		var resultTask = await Task.WhenAny(task, tcs.Task);
		if (resultTask == tcs.Task)
		{
			// Operation cancelled
			throw new OperationCanceledException(cancellationToken);
		}

		return await task;
	}
}