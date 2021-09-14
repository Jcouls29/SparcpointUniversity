// See https://aka.ms/new-console-template for more information
public class TransientServiceDependency : IDisposable
{
    public TransientServiceDependency()
    {
        Console.WriteLine($"{nameof(TransientServiceDependency)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(TransientServiceDependency)}.Dispose()");
    }
}
