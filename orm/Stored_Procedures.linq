using (var context = new YourDbContext())
{
	// Call a stored procedure using DbContext.Database.ExecuteSqlCommand
	int rowsAffected = context.Database.ExecuteSqlCommand("EXEC YourStoredProcedure @Parameter1, @Parameter2",
		new SqlParameter("@Parameter1", value1),
		new SqlParameter("@Parameter2", value2));

	// Execute a stored procedure and map the result to entities
	var results = context.YourEntities
		.FromSql("EXEC YourStoredProcedure @Parameter1, @Parameter2",
			new SqlParameter("@Parameter1", value1),
			new SqlParameter("@Parameter2", value2))
		.ToList();

	// Access the results
	// ...
}