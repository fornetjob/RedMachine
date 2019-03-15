using Assets.Scripts.Features.Pooling;

public interface IPoolListener<T>
    where T : IComponent, new()
{
    void OnChange(ComponentPool<T> pool);
}