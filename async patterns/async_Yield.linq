<Query Kind="Expression">
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

public async Task PerformLongRunningOperationAsync()
{
	for (int i = 0; i < 100; i++)
	{
		// Perform a partial computation
		DoPartialComputation();

		// Yield control back to the calling context (e.g., the UI thread)
		await Task.Yield();
	}
}

public async Task PerformLongRunningOperationAsync()
{
	for (int i = 0; i < 100; i++)
	{
		// Perform a partial computation that requires shared resources
		DoPartialComputationWithSharedResource();

		// Yield control back to the calling context to allow other tasks to access shared resources
		await Task.Yield();
	}
}