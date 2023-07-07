
async Task Main()
{
	await ThisWillCatchTheException();
}

public async Task AsyncTaskMethodThrowsException()
{
	throw new Exception("Hmmm, something went wrong!");
}

public async Task ThisWillCatchTheException()
{
	try
	{
		await AsyncTaskMethodThrowsException();
	}
	catch (Exception ex)
	{
		//The below line will actually be reached
		Debug.WriteLine(ex.Message);
	}
}