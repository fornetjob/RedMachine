namespace Assets.Scripts.Features.Pooling
{
    public interface IPoolItem:IDestroy
    {
        void Attach(IPool pool);
        int Id { get; set; }
    }
}