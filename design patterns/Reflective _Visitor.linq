<Query Kind="Program">
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

class Program
{
	static void Main(string[] args)
	{
		Operand<int> a = new Operand<int>(21);
		Operand<short> b = new Operand<short>(21);
		Console.WriteLine(Add(a, b));
	}

	static IOperand Add(IOperand a, IOperand b)
	{
		AdditionVisitor addVisitor = new AdditionVisitor();
		return a.Accept(addVisitor, b);
	}
}

public interface IOperand
{
	IOperand Accept(Visitor visitor, IOperand right);
}

public class Operand<T> : IOperand
{
	private T _value;

	public Operand(T value)
	{
		_value = value;
	}
	public IOperand Accept(Visitor v, IOperand right)
	{
		return v.Visit(this, right);
	}
	public T Value
	{
		get { return _value; }
	}
	public override string ToString()
	{
		return string.Format("{0} ({1})", _value, GetType());
	}
}

public class Visitor
{
	public virtual IOperand Visit(IOperand left, IOperand right)
	{
		MethodInfo info = GetType().GetMethod("Visit", new Type[] { left.GetType(), right.GetType() });

		if (info != null && info.DeclaringType != typeof(Visitor))
			return info.Invoke(this, new object[] { left, right }) as IOperand;

		Console.WriteLine("Operation not supported");
		return null;
	}
}


public class AdditionVisitor : Visitor
{
	public IOperand Visit(Operand<int> value, Operand<int> right)
	{
		return new Operand<int>(value.Value + right.Value);
	}
	public IOperand Visit(Operand<int> value, Operand<short> right)
	{
		return new Operand<int>(value.Value + right.Value);
	}
	public IOperand Visit(Operand<double> value, Operand<int> right)
	{
		return new Operand<double>(value.Value + right.Value);
	}
}