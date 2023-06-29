
public interface IUnitOfWork : IDisposable
{
	void BeginTransaction();

	void RollbackTransaction();

	void CommitTransaction();

	Task<bool> SaveChangesAsync();

}

public class VendorDBContext : DbContext, IUnitOfWork
{


	public VendorDBContext(DbContextOptions options) : base(options)
	{

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		//  optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=FraymsVendorDB;Integrated Security=False; User Id=sa; Password=P@ssw0rd; Timeout=500000;");
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}

	public void BeginTransaction()
	{
		this.Database.BeginTransaction();
	}
	public void RollbackTransaction()
	{
		this.Database.RollbackTransaction();
	}
	public void CommitTransaction()
	{
		this.Database.CommitTransaction();
	}
	public Task<bool> SaveChangesAsync()
	{
		return this.SaveChangesAsync();
	}

	#region Entities representing Vendor Domain Objects
	public DbSet<VendorMaster> VendorMaster { get; set; }
	public DbSet<VendorDocument> VendorDocuments { get; protected set; }
	#endregion
}

public class Worker
{
	public void DoSomeWork()
	{
		using (var unitOfWork = new UnitOfWork())
		{
			// Perform unit of work here.
		}
		// At this point the unit of work object has been disposed of.
	}
}

public class Worker
{
	public void DoSomeWork()
	{
		using (var unitOfWork = new UnitOfWork())
		{
			// Perform unit of work here.
		}
		// At this point the unit of work object has been disposed of.
	}
}

public class UnitOfWork : IDisposable
{
	#region IDisposable Support
	private bool disposedValue = false; // To detect redundant calls

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects).
			}

			// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
			// TODO: set large fields to null.

			disposedValue = true;
		}
	}

	// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
	// ~UnitOfWork()
	// {
	//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
	//   Dispose(false);
	// }

	// This code added to correctly implement the disposable pattern.
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		Dispose(true);
		// TODO: uncomment the following line if the finalizer is overridden above.
		// GC.SuppressFinalize(this);
	}
	#endregion
}