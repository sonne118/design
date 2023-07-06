class Program
{
	static void Main(string[] args)
	{
		var account1 = new Account(100);
		var account2 = new Account(50);

		Task transfer1 = account1.TransferAsync(account2, 25);
		Task transfer2 = account2.TransferAsync(account1, 10);

		Task.WaitAll(transfer1, transfer2);

		Console.WriteLine($"Account 1 balance: {account1.Balance}");
		Console.WriteLine($"Account 2 balance: {account2.Balance}");
	}
}

class Account
{
	
	//private static readonly object _balanceLock = new object();
	private static readonly SemaphoreSlim _balanceSemaphore = new SemaphoreSlim(1);
	
	public int Balance { get; private set; }

	public Account(int balance)
	{
		Balance = balance;
	}

	public async Task TransferAsync(Account otherAccount, int amount)
	{
		await Task.Delay(100); // simulate network delay

		//lock (_balanceLock)
		//{}
		await _balanceSemaphore.WaitAsync();
		try
		{
			Balance -= amount;
			otherAccount.Balance += amount;
		}
		finally
		{
			_balanceSemaphore.Release();
		}


	}	
}