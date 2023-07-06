public class MyDbContext : DbContext
{
	public MyDbContext()
	{
		Configuration.AutoDetectChangesEnabled = false;
	}

	// Your DbSet properties and other code
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			// Perform your operations on entities

			// Save changes
			context.SaveChanges();
		}
	}
}