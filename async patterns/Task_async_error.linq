void Main()
{
	StartAsync();
}
async Task StartAsync()
{
	await CatchBlockWithNotBeExecute();
}

async Task CatchBlockWithNotBeExecute()
{

	try
	{
		await Task.FromException(new Exception("Help"));
	}
	catch (Exception ex)
	{
		Console.WriteLine("-------");
		throw;
	}
}

//Handling Exceptions
async Task CatchBlock(Task task)
{

	try
	{
		await task;
	}
	catch (Exception ex)
	{
		// log ex.Message
	}
	task.ContinueWith((t) =>
	{
		// log ex.InnerException.Message
	}, TaskContinuationOptions.OnlyOnFaulted)
}

//continuing After an Exception

Task Continuing()
{
	var loadLinesTask = Task.Run(() =>
{
	throw new FileNotFoundException();
});
	loadLinesTask.ContinueWith((completedTask) =>
	{
		// will always run
	});
	loadLinesTask.ContinueWith((completedTask) =>
	{
		// will not run if completedTask is faulted
	}, TaskContinuationOptions.OnlyOnRanToCompletion);
	return loadLinesTask;
}
