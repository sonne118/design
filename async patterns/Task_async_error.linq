<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <RuntimeVersion>6.0</RuntimeVersion>
</Query>

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
		   await  Task.FromException(new Exception("Help"));
		}
		catch (Exception ex)
		{
			Console.WriteLine("-------");
			throw;
		}
	}


// You can define other methods, fields, classes and namespaces here