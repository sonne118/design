
void Main()
{
	BenchmarkRunner.Run<PoolBenchmark>();
}

public class ObjectPool<T>
{
	private readonly ConcurrentBag<T> _objects;
	private readonly Func<T> _objectGenerator;

	/// <summary>
	/// Initializes the ObjectPool.
	/// </summary>
	/// <param name="objectGenerator">We need a generator function to create an object if our pool is empty.</param>
	public ObjectPool(Func<T> objectGenerator)
	{
		_objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
		_objects = new ConcurrentBag<T>();
	}

	public T Rent() => _objects.TryTake(out T item) ? item : _objectGenerator();

	public void Return(T item) => _objects.Add(item);
		
}

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class PoolBenchmark
{
	private const int LoopCount = 10;

	[BenchmarkDotNet.Attributes.Benchmark(Baseline = true)]
	public int CreateNewObjects()
	{
		var sum = 0;
		for (var i = 0; i < LoopCount; i++)
		{
			var obj = new MyHeavyClass();
			sum += obj.Length;
		}

		return sum;
	}

	[Benchmark]
	public int ObjectPool()
	{
		var pool = new ObjectPool<MyHeavyClass>(() => new MyHeavyClass());
		var sum = 0;
		for (var i = 0; i < LoopCount; i++)
		{
			var obj = pool.Rent();
			sum += obj.Length;
			pool.Return(obj);
		}

		return sum;
	}
}

public class MyHeavyClass
{
	private readonly double[] _someArray;

	public MyHeavyClass()
	{
		_someArray = new double[5000];
		Array.Fill(_someArray, 10);
	}

	public int Length => _someArray.Length;
}


//|           Method |      Mean |     Error |    StdDev | Ratio | RatioSD |    Gen0 |   Gen1 |   Gen2 | Allocated | Alloc Ratio |
//|----------------- |----------:|----------:|----------:|------:|--------:|--------:|-------:|-------:|----------:|------------:|
//| CreateNewObjects | 25.965 us | 0.4657 us | 0.7908 us |  1.00 |    0.00 | 47.6074 | 5.9204 |      - | 391.09 KB |        1.00 |
//|       ObjectPool |  9.724 us | 0.1943 us | 0.4502 us |  0.38 |    0.02 |  4.8218 | 2.4109 | 1.2054 |  39.65 KB |        0.10 |
