<Query Kind="Expression">
  <Output>DataGrids</Output>
  <NuGetReference>App.Metrics.AspNetCore</NuGetReference>
  <NuGetReference>App.Metrics.AspNetCore.Core</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http.Features</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Mvc</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Configuration</NuGetReference>
  <NuGetReference>Microsoft.Extensions.DependencyInjection</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Http.Polly</NuGetReference>
  <NuGetReference>NUnitLite</NuGetReference>
  <NuGetReference>System.Threading.Tasks.Dataflow</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public void TransferFunds(Account sourceAccount, Account destinationAccount, decimal amount)
{
	using (var dbContext = new ApplicationDbContext())
	{
		using (var transaction = dbContext.Database.BeginTransaction())
		{
			try
			{
				// Retrieve source and destination accounts
				var source = dbContext.Accounts.Find(sourceAccount.Id);
				var destination = dbContext.Accounts.Find(destinationAccount.Id);

				// Perform the funds transfer within the transaction
				source.Balance -= amount;
				destination.Balance += amount;

				// Save changes to the database
				dbContext.SaveChanges();

				// Commit the transaction if all operations succeed
				transaction.Commit();
			}
			catch (Exception ex)
			{
				// Handle any exceptions and rollback the transaction if necessary
				transaction.Rollback();
				throw ex;
			}
		}
	}
}