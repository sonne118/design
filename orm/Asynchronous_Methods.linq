public async Task<List<Customer>> GetCustomersAsync()
{
	using (var dbContext = new ApplicationDbContext())
	{
		// Use asynchronous method to query customers
		//Creating an DbContext instance using method AddDbContext, instance is not thread safe, 
		// which means same instance cannot be used for multiple queries running at the same time
		return await dbContext.Customers.ToListAsync();
	}
}
