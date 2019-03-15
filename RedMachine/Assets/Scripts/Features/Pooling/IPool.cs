namespace Assets.Scripts.Features.Pooling
{
    public interface IPool
    {
        bool ContainsId(int id);
        IPoolItem GetById(int id);

        void Destroy(IPoolItem item);
    }
}
