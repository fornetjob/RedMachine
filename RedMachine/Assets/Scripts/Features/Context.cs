using Assets.Scripts.Core;
using Assets.Scripts.Features;
using Assets.Scripts.Features.Events;
using Assets.Scripts.Features.Pooling;

public class Context
{
    #region ctor

    public Context(params IService[] mockServices)
    {
        systems = new SystemsComposite(this);

        services = new ContextServices(mockServices);

        entities = new ComponentPool<Entity>(this);

        services.Attach(this);

        systems.Add(new EventSystem());
        systems.Add(new PoolSystem());
    }

    #endregion

    #region Properties

    public readonly SystemsComposite systems;
    public readonly ComponentPool<Entity> entities;

    #endregion
    public readonly ContextServices services;
}