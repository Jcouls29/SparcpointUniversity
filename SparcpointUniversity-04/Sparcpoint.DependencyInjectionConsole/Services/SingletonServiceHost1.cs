// See https://aka.ms/new-console-template for more information

public class SingletonServiceHost1 : IDisposable
{
    public SingletonServiceHost1(SingletonServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(SingletonServiceHost1)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(SingletonServiceHost1)}.Dispose()");
    }
}
