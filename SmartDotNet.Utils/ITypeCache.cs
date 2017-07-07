namespace SmartDotNet.Utils
{
    public interface ITypeCache<TPar, TRes>
    {
        TRes Get(TPar par);
        void Set(TPar par, TRes res);
    }
}