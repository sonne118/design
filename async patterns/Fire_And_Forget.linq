public async Task DoSomethingAsync()
{
	// Code before the Task.Run call
	Task.Run(async () =>
	{
		// Asynchronous code to be executed on a thread pool thread
		await LogToDatabaseAsync();
	});
	// Code after the Task.Run call
}

private async Task LogToDatabaseAsync()
{
	// Asynchronous code to log to a database
	await Task.Delay(3000);
}
//----------------------------------------------------------------
// The Best way  It can be improved by simply ignoring the task in C# using the discard feature.
public async Task DoSomethingAsync()
{
	Console.WriteLine("Starting DoSomethingAsync");

	// Call an asynchronous method to log to a database using fire-and-forget
	_ = LogToDatabaseAsync();

	Console.WriteLine("DoSomethingAsync completed successfully");
}

private async Task LogToDatabaseAsync()
{
	// Simulate a long-running database operation
	await Task.Delay(3000);
	Console.WriteLine("Logged to database successfully");
}