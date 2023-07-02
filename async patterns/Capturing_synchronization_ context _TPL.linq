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

async Task Main(string[] args)
{
	await SomeUIMethod();
}

public  async Task SomeMethod1()
{
	var t = Task.Delay(TimeSpan.FromSeconds(1))
				.ContinueWith(
					_ => Task.Delay(TimeSpan.FromSeconds(42))
				);
	await t;
}


public  async Task SomeMethod2()
{
	await Task.Delay(TimeSpan.FromSeconds(1));
	await Task.Delay(TimeSpan.FromSeconds(42));
}

//Capturing the synchronization context in TPL tasks
private async Task SomeUIMethod()
{
	string Title = "Done !";
	var t = Task.Delay(TimeSpan.FromSeconds(1))
				.ContinueWith(
					_ => Title = "Done !",
					// Specify where to execute the continuation
					TaskScheduler.FromCurrentSynchronizationContext()
				);

	 await t;
}