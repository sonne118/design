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
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

void Main()
{
	 Run();							
}

BufferBlock<int> buffer = new BufferBlock<int>(
			new DataflowBlockOptions { BoundedCapacity = 10 });

async Task Produce(IEnumerable<int> values)
{
	foreach (var value in values)
		await buffer.SendAsync(value); ;
}

async Task MultipleProducers(params IEnumerable<int>[] producers)
{
	await Task.WhenAll(
			(from values in producers select Produce(values)).ToArray())
		.ContinueWith(_ => buffer.Complete());
}

async Task Consumer(Action<int> process)
{
	while (await buffer.OutputAvailableAsync())
		process(await buffer.ReceiveAsync());
}

public async Task Run()
{
	IEnumerable<int> range = Enumerable.Range(0, 100);

	await Task.WhenAll(MultipleProducers(range, range, range),
		Consumer(n => Console.WriteLine($"value {n} - ThreadId{Thread.CurrentThread.ManagedThreadId}")));
}