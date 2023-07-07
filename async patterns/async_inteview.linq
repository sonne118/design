<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


namespace Interview
{
	class Program
	{
		static async Task Main()
		{
			Console.WriteLine("Before async");//1
			var fooAsync = FooAsync();
			Console.WriteLine("After async");//3
			var fooAsyncWithWait = FooAsyncWithAwait();
			Console.WriteLine("Before await"); //4
			await fooAsync;
			await fooAsyncWithWait;
			Console.WriteLine("After await"); //7
			Console.WriteLine("DONE"); //9
		}

		static async Task FooAsync()
		{
			Task.Delay(1000).Wait();
			Console.WriteLine("Async method that doesn't have await"); //2 //6
		}

		static async Task FooAsyncWithAwait()
		{
			await Task.Delay(1000);
			Console.WriteLine("Async method that have await"); //5 //8
		}

	}
}
