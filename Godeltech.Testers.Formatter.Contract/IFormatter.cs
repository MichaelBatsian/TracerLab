namespace Godeltech.Testers.Formatter.Contract
{
    public interface IFormatter<T>
    {
        string GetName();
        void Format(TreeNode<T> tree, ITracer tracer, int level, bool isRoot, string savePath);
    }

}
