using Assets.Scripts.Features;
using System.Collections.Generic;

public class Context: IFixedUpdateSystem, IUpdateSystem
{
    #region Fields

    private List<IFixedUpdateSystem>
        _fixedUpdateSystems = new List<IFixedUpdateSystem>();

    private List<IUpdateSystem>
        _updateSystems = new List<IUpdateSystem>();

    #endregion

    public Context()
    {
        services = new ContextServices();

        services.Attach(this);
    }

    public ContextServices services;

    public void RemoveSystem(ISystem system)
    {
        if (system is IFixedUpdateSystem)
        {
            _fixedUpdateSystems.Remove((IFixedUpdateSystem)system);
        }

        if (system is IUpdateSystem)
        {
            _updateSystems.Remove((IUpdateSystem)system);
        }
    }

    public void AddSystem(ISystem system)
    {
        if (system is IAttachContext)
        {
            ((IAttachContext)system).Attach(this);
        }

        if (system is IStartSystem)
        {
            ((IStartSystem)system).OnStart();
        }

        if (system is IFixedUpdateSystem)
        {
            _fixedUpdateSystems.Add((IFixedUpdateSystem)system);
        }

        if (system is IUpdateSystem)
        {
            _updateSystems.Add((IUpdateSystem)system);
        }
    }

    void IFixedUpdateSystem.OnFixedUpdate()
    {
        for (int i = 0; i < _fixedUpdateSystems.Count; i++)
        {
            _fixedUpdateSystems[i].OnFixedUpdate();
        }
    }

    void IUpdateSystem.OnUpdate()
    {
        for (int i = 0; i < _updateSystems.Count; i++)
        {
            _updateSystems[i].OnUpdate();
        }
    }
}