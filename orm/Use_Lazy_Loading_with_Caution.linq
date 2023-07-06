
using (var context = new YourDbContext())
{
	// Disable lazy loading explicitly
	context.Configuration.LazyLoadingEnabled = false;

	// Load related entities explicitly using Include method
	var entity = context.YourEntities
		.Include(e => e.RelatedEntity)
		.FirstOrDefault();

	// Access the related entities
	// ...

	// Explicitly load related entities using Load method
	context.Entry(entity)
		.Reference(e => e.RelatedEntity)
		.Load();

	// Access the loaded related entity
	// ...

	// Avoid accessing navigation properties outside the context scope
	// ...

	// Consider using eager loading or explicit loading instead of lazy loading
	// ...

	// Dispose the context when you're done
	// ...
}