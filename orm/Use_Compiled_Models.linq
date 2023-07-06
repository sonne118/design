public class MyDbContext : DbContext
{
	public MyDbContext()
		: base("YourConnectionString")
	{
		var compiledModel = DbModelBuilder.Build(typeof(MyDbContext).Assembly);
		var compiledDbModel = compiledModel.Compile();

		var existingConnection = this.Database.Connection;
		var newConnection = DbProviderServices.GetProviderFactory(existingConnection).CreateConnection();
		newConnection.ConnectionString = existingConnection.ConnectionString;

		var compiledDbConnection = compiledDbModel.Compile(newConnection);

		Database.DefaultConnectionFactory = new SqlConnectionFactory(newConnection.ConnectionString);
		Database.SetInitializer<MyDbContext>(null);
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