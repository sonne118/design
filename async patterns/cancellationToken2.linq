public async Task<string> DoAsyncThing(CancellationToken cancellationToken = default)
{
	byte[] buffer = new byte[1024];
	// This properly flows cancellationToken to ReadAsync
	int read = await _stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
	return Encoding.UTF8.GetString(buffer, 0, read);
}