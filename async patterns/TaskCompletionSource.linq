<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	ExecuteTask();
}

static Task<int> ExecuteTask()
{
	var tcs = new TaskCompletionSource<int>();
	Task<int> t1 = tcs.Task;
	Task.Factory.StartNew(() =>
	{
		try
		{
			ExecuteLongRunningTask(1000);
			tcs.SetResult(1);
		}
		catch (Exception ex)
		{
			tcs.SetException(ex);
		}
	}
	);
	return tcs.Task;

}

 static void ExecuteLongRunningTask(int millis)
{
	Thread.Sleep(1000);
	Console.WriteLine("Executed");
}
