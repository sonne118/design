
public async Task<long> ProcessDataAsync()
{
	
	//to offload the work to the ThreadPool to prevent blocking the primary thread.
	return await Task.Run(() =>
	{
		// Perform long-running CPU-bound operation
		var result = ComputeResult();
		return result;
	});
}