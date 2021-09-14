// See https://aka.ms/new-console-template for more information
public class ScopedServiceHost2 : IDisposable
{
    public ScopedServiceHost2(ScopedServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(ScopedServiceHost2)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(ScopedServiceHost2)}.Dispose()");
    }
}