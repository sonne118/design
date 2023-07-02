<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

	static async Task Main()
	{
		var al = new AsyncLocal<int>() { Value = 1 };
		for (int i = 0; i < 1000; i++)
		{
			await SomeMethodAsync();
			al.Value ++;
		}
		
		Console.WriteLine(al);
	}

	static async Task SomeMethodAsync()
	{
		for (int i = 0; i < 1000; i++)
		{			
			await Task.Yield();
		}
	}
