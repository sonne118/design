<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Person person = null;
				var program = new Program();
				//program.TryCatchExample(person);
				program.ArgumentNullValidatorExample(person);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
			finally
			{
				//Console.ReadKey();
			}
		}

		private void TryCatchExample(Person person)
		{
			try
			{
				Console.WriteLine($"Person's Name: {person.Name}");
			}
			catch (NullReferenceException nre)
			{
				Console.WriteLine("Error: The person argument cannot be null.");
				throw;
			}
		}

		private void ArgumentNullValidatorExample(Person person)
		{
			ArgumentNullValidator.NotNull("Person", person);
			Console.WriteLine($"Person's Name: {person.Name}");
		}
	}

internal static class ArgumentNullValidator
{
	public static void NotNull(string name, [ValidatedNotNull] object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}
}


public class Person
	{
		public string Name { get; }

		public Person(string name)
		{
			Name = name;
		}
	}

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
internal sealed class ValidatedNotNullAttribute : Attribute
{
}


