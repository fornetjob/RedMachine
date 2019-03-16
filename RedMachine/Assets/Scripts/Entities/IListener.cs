public interface IListener<T>
{
    void OnChanged(T value);
}
