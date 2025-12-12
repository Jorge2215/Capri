namespace Pampa.InSol.Test.Builders
{
    internal interface IBuilder<T> where T : class
    {
        T Build();
    }
}
