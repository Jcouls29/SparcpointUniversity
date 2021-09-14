// See https://aka.ms/new-console-template for more information
public class ScopedServiceHost1 : IDisposable
{
    public ScopedServiceHost1(ScopedServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(ScopedServiceHost1)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(ScopedServiceHost1)}.Dispose()");
    }
}
