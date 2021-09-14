// See https://aka.ms/new-console-template for more information
public class SingletonServiceDependency : IDisposable
{
    public SingletonServiceDependency()
    {
        Console.WriteLine($"{nameof(SingletonServiceDependency)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(SingletonServiceDependency)}.Dispose()");
    }
}
