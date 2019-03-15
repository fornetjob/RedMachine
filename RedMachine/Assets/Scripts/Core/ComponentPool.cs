using Assets.Scripts.Features.Pooling;

public class ComponentPool<T> : Pool<ComponentProperty<T>>
    where T : IComponent, new()
{
}