[Component(IsIdentity = false)]
public class EventComponent : ComponentBase
{
    public string name;
    public object value;

    public void Set(string name, object value)
    {
        this.name = name;
        this.value = value;
    }
}