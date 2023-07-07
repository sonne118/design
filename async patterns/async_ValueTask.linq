
public async ValueTask<int> PerformOperationAsync()
{
	if (cache.TryGetFromCache(out int result))
		return result;

	result = await ComputeResultAsync();
	cache.StoreInCache(result);
	return result;
}