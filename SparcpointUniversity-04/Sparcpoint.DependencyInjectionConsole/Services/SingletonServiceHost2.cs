// See https://aka.ms/new-console-template for more information


public class SingletonServiceHost2 : IDisposable
{
    public SingletonServiceHost2(SingletonServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(SingletonServiceHost2)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(SingletonServiceHost2)}.Dispose()");
    }
}
