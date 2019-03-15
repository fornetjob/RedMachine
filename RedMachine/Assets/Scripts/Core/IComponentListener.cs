public interface IComponentListener<T>
    where T : IComponent
{
    void OnChanged(T newValue);
}