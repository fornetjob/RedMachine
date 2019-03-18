public interface IPool
{
    void ApplyChanges();
    void OnChanged(int id);

    bool ContainsId(int id);
    IComponent GetById(int id);

    void Destroy(IComponent item);
    void DestroyAll();
}