// See https://aka.ms/new-console-template for more information
public class TransientServiceHost2 : IDisposable
{
    public TransientServiceHost2(TransientServiceDependency dependency)
    {
        Console.WriteLine($"{nameof(TransientServiceHost2)}()");
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(TransientServiceHost2)}.Dispose()");
    }
}