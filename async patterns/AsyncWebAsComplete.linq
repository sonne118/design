<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net</Namespace>
</Query>



async Task Main()
{
	List<string> urlList = new List<string>
			{
				"http://www.google.com",
				"http://www.amazon.com",
				"http://msdn.microsoft.com",
				"http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
				"http://msdn.microsoft.com/en-us/library/hh290136.aspx",
				"http://msdn.microsoft.com/en-us/library/dd470362.aspx",
				"http://msdn.microsoft.com/en-us/library/aa578028.aspx",
				"http://msdn.microsoft.com/en-us/library/ms404677.aspx",
				"http://msdn.microsoft.com/en-us/library/ff730837.aspx"
			};
			
	IEnumerable<Task<int>> downloadTasksQuery =
		  from url in urlList select GetSiteSize(new Uri(url));

	// ***Use ToList to execute the query and start the tasks. 
	List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

	// ***Add a loop to process the tasks one at a time until none remain.
	while (downloadTasks.Count > 0)
	{
		// Identify the first task that completes.
		Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

		// ***Remove the selected task from the list so that you don't
		// process it more than once.
		downloadTasks.Remove(firstFinishedTask);

		// Await the completed task.
		int length = await firstFinishedTask;
		length.Dump();
	}
}

async Task<int> GetSiteSize(Uri uri)
{
	("Downloading " + uri + "...").Dump();
	string html = await new WebClient().DownloadStringTaskAsync(uri);

	var otherFiles =
		from Match m in SrcMatch.Matches(html)
		select m.Groups[1].Value;

	var otherFileLengths =
		from otherPage in otherFiles.Distinct().Dump("(these are the other URIs)")
		select new WebClient().DownloadDataTaskAsync(new Uri(uri, otherPage));

	// DownloadDataTaskAsync returns a Task<byte[]>, therefore when we await WhenAll on
	// an array of them, we'll end up with an array of byte[], in other words a byte[][].

	byte[][] fileContents = await Task.WhenAll(otherFileLengths.Take(1));
	return html.Length + fileContents.Sum(fc => fc.Length);
}

Regex SrcMatch = new Regex(@"src\s*=\s*['""](.*?\.(png|gif|png|jpg|js))['""]", RegexOptions.IgnoreCase);