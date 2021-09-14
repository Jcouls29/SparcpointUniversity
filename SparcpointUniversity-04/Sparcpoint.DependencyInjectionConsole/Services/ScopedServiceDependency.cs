// See https://aka.ms/new-console-template for more information
public class ScopedServiceDependency : IDisposable
{
    public ScopedServiceDependency()
    {
        Console.WriteLine($"{nameof(ScopedServiceDependency)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(ScopedServiceDependency)}.Dispose()");
    }
}