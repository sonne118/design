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
		var tasks = from url in urlList	
						  select GetSiteSize(new Uri(url));
		

	foreach (var task in tasks)
	{
		int size = await task;
		size.Dump();
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


	byte[][] fileContents = await Task.WhenAll(otherFileLengths.Take(1));
	return html.Length + fileContents.Sum(fc => fc.Length);
}

Regex SrcMatch = new Regex(@"src\s*=\s*['""](.*?\.(png|gif|png|jpg|js))['""]", RegexOptions.IgnoreCase);