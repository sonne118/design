public class YourDbContext : DbContext
{
	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		// Configure indexes on specific properties or columns
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.SomeProperty)
			.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

		// Alternatively, you can use fluent API to define indexes
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.SomeProperty)
			.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));

		// Configure composite indexes on multiple properties or columns
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.Property1)
			.HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute(), new IndexAttribute() }));
		modelBuilder.Entity<YourEntity>()
			.Property(e => e.Property2)
			.HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute(), new IndexAttribute() }));
	}
}