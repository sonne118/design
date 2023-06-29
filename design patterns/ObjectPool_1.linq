
void Main()
{
	BenchmarkRunner.Run<PoolBenchmark>();
}
/*
     * Based on ObjectPool<T> from Roslyn source code (with comments reused):
     * https://github.com/dotnet/roslyn/blob/d4dab355b96955aca5b4b0ebf6282575fad78ba8/src/Dependencies/PooledObjects/ObjectPool%601.cs
     */
    public class ObjectPool<T> where T : class
    {
        private T firstItem;
        private readonly T[] items;
        private readonly Func<T> generator;

        public ObjectPool(Func<T> generator, int size)
        {
            this.generator = generator ?? throw new ArgumentNullException("generator");
            this.items = new T[size - 1];
        }

        public T Rent()
        {
            // PERF: Examine the first element. If that fails, RentSlow will look at the remaining elements.
            // Note that the initial read is optimistically not synchronized. That is intentional. 
            // We will interlock only when we have a candidate. in a worst case we may miss some
            // recently returned objects. Not a big deal.
            Console.WriteLine("R:");
            T inst = firstItem;
            if (inst == null || inst != Interlocked.CompareExchange(ref firstItem, null, inst))
            {
                inst = RentSlow();
            }
            return inst;
        }

        public void Return(T item)
        {
            Console.WriteLine("*");
            if (firstItem == null)
            {
                // Intentionally not using interlocked here. 
                // In a worst case scenario two objects may be stored into same slot.
                // It is very unlikely to happen and will only mean that one of the objects will get collected.
                firstItem = item;
            }
            else
            {
                ReturnSlow(item);
            }
        }

        private T RentSlow()
        {
            for (int i = 0; i < items.Length; i++)
            {
                // Note that the initial read is optimistically not synchronized. That is intentional. 
                // We will interlock only when we have a candidate. in a worst case we may miss some
                // recently returned objects. Not a big deal.
                T inst = items[i];
                if (inst != null)
                {
                    if (inst == Interlocked.CompareExchange(ref items[i], null, inst))
                    {
                        Console.WriteLine("  -");
                        return inst;
                    }
                }
            }
            Console.WriteLine("  --");
            return generator();
        }

        private void ReturnSlow(T obj)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    // Intentionally not using interlocked here. 
                    // In a worst case scenario two objects may be stored into same slot.
                    // It is very unlikely to happen and will only mean that one of the objects will get collected.
                    items[i] = obj;
                    break;
                }
            }
        }
    }

[MemoryDiagnoser]
public class PoolBenchmark
{
	private const int LoopCount = 10;

	[Benchmark(Baseline = true)]
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
		var pool = new ObjectPool1<MyHeavyClass>(() => new MyHeavyClass(), 10);
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


//|           Method |        Mean |     Error |     StdDev |  Ratio | RatioSD |    Gen0 |   Gen1 | Allocated | Alloc Ratio |
//|----------------- |------------:|----------:|-----------:|-------:|--------:|--------:|-------:|----------:|------------:|
//| CreateNewObjects |    24.54 us |  0.432 us |   0.404 us |   1.00 |    0.00 | 47.6074 | 5.9204 | 391.09 KB |        1.00 |
//|       ObjectPool | 2,750.74 us | 53.821 us | 107.487 us | 114.42 |    5.43 |  3.9063 |      - |  39.24 KB |        0.10 |
