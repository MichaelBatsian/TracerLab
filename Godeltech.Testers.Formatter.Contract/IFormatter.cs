using Godeltech.Data.Structures;

namespace Godeltech.Testers.Formatter.Contract
{
    public interface IFormatter<T>
    {
        string GetName();
        void Format( T obj, string savePath);
    }
}
