public class MyDbContext : DbContext
{
	// Your DbSet properties

	public IEnumerable<TEntity> GetEntitiesWithPagination<TEntity>(int pageNumber, int pageSize)
		where TEntity : class
	{
		return Set<TEntity>()
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToList();
	}
}

public class Program
{
	public static void Main()
	{
		using (var context = new MyDbContext())
		{
			// Get entities with pagination
			int pageNumber = 1;
			int pageSize = 10;
			var entities = context.GetEntitiesWithPagination<TEntity>(pageNumber, pageSize);

			// Process the entities
			foreach (var entity in entities)
			{
				// Do something with the entity
			}
		}
	}
}