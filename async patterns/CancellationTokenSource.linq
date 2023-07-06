public async Task<Stream> HttpClientAsyncWithCancellationGood()
{
	using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
	{
		using (var client = _httpClientFactory.CreateClient())
		{
			var response = await client.GetAsync("http://backend/api/1", cts.Token);
			return await response.Content.ReadAsStreamAsync();
		}
	}
}