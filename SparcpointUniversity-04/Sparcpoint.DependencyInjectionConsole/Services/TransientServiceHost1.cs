// See https://aka.ms/new-console-template for more information
public class TransientServiceHost1 : IDisposable
{
    public TransientServiceHost1(TransientServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(TransientServiceHost1)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(TransientServiceHost1)}.Dispose()");
    }
}
