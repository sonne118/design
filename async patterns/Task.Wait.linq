async Task Main()
{
  await DemoTaskWaitAsync();
}
	async Task<string> DemoTaskWaitAsync()
	{
		Console.WriteLine("Starting DemoTaskWaitAsync");
	
		// Call an async method using Task.Run to simulate a long-running operation
		Task<string> longRunningTask = Task.Run(() => DoSomethingAsync());
	
		// Wait for the long-running task to complete or timeout after 2 seconds
		if (!longRunningTask.Wait(2000))
		{
			// If the task timed out, return a string indicating so
			Console.WriteLine("DemoTaskWaitAsync timed out!");
			return "Timed out";
		}
	
		// If the task completed within the timeout period, return its result
		Console.WriteLine("DemoTaskWaitAsync completed successfully");
		return await longRunningTask;
	}
	
	 async Task<string> DoSomethingAsync()
	{
		Console.WriteLine("Starting DoSomethingAsync");
		await Task.Delay(3000);
		Console.WriteLine("DoSomethingAsync completed successfully");
		return "Done";
	}

