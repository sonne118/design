class DataManager : IDisposable
{
	public void Dispose()
	{
		Console.WriteLine("Disposing object");
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(Boolean disposing)
	{
		if (disposing)
		{
			if (_conn != null)
			{
				_conn.Close();
				_conn.Dispose();
				//set _conn to null, so next time it won't hit this block
				_conn = null;
			}
		}
	}

	~DataManager()
	{
		Dispose(false);
	}
}

GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
GC.Collect();
GC.Collect();
GC.Collect();

Console.WriteLine("done");
Console.Read();