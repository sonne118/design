<Query Kind="Program">
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

async Task Main()
{
  await DemoTaskWaitAsync();
}
	public async Task<string> DemoTaskWaitAsync()
	{
		Console.WriteLine("Starting DemoTaskWaitAsync");
	
		string response = string.Empty;
	
		// Call an async method using Task.Run to simulate a long-running operation
		Task<string> longRunningTask = Task.Run(() => DoSomethingAsync());
	
		// Wait for either the long-running task to complete or a timeout of 2 seconds
		try
		{
			response = await longRunningTask.WaitAsync(TimeSpan.FromSeconds(2));//4
		}
		catch (TimeoutException)
		{
			Console.WriteLine("DemoTaskWaitAsync timed out!");
			return "Timed out";
		}
	
		// If the winner is the long-running task, return its result
		Console.WriteLine("DemoTaskWaitAsync completed successfully");
		return response;
	}
	
	private async Task<string> DoSomethingAsync()
	{
		Console.WriteLine("Starting DoSomethingAsync");
		await Task.Delay(3000);
		Console.WriteLine("DoSomethingAsync completed successfully");
		return "Done";
	}


