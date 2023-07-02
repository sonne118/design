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
	await Test();

	var t = Task.Run(async () => await Test());

	await t;
}

public async Task Test()
{
	Debug.WriteLine("SyncContext : {0}", SynchronizationContext.Current);

	Debug.WriteLine("1. {0}", Thread.CurrentThread.ManagedThreadId);

	await Task.Delay(1000);

	Debug.WriteLine("2. {0}", Thread.CurrentThread.ManagedThreadId);

	await Task.Delay(1000);

	Debug.WriteLine("3. {0}", Thread.CurrentThread.ManagedThreadId);

	await Task.Delay(1000);

	Debug.WriteLine("4. {0}", Thread.CurrentThread.ManagedThreadId);
}

//SyncContext:
//1. 1
//2. 5
//3. 5
//4. 5
//SyncContext:
//1. 5
//2. 5
//3. 5
//4. 5