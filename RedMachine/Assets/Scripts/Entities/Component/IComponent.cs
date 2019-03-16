public interface IComponent: IDestroy
{
    bool IsChanged { get; set; }
    void Attach(IPool pool);
    int Id { get; set; }
}