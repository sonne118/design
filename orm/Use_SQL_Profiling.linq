public class MyDbContext : DbContext
{
	// Your DbSet properties

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// Set up your database connection string
		optionsBuilder.UseSqlServer("YourConnectionString");

		// Enable SQL logging
		optionsBuilder.EnableSensitiveDataLogging(true);
	}
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			// Your database operations

			// Access the logged SQL statements
			var log = context.GetService<Microsoft.Extensions.Logging.ILoggerFactory>()
				.CreateLogger<MyDbContext>();
			var logs = log.LoggedSqlStatements;

			// Print the logged SQL statements
			foreach (var sql in logs)
			{
				Console.WriteLine(sql);
			}
		}
	}
}