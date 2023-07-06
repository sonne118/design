public class YourDbContext : DbContext
{
	public IQueryable<YourEntity> GetEntities()
	{
		// Retrieve entities directly from the database without loading into memory
		return Set<YourEntity>().AsNoTracking();
	}
}