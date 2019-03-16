using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Unit;
using NUnit.Framework;

public class ComponentPoolTest
{
    [Test]
    public void Pooling()
    {
        var context = new Context();

        var pool = context.services.pool.Provide<MoveComponent>();

        var set = pool.Create();

        Assert.AreEqual(pool.Items.Count, 1);

        set.Destroy();

        Assert.AreEqual(pool.Items.Count, 0);
    }

    [Test]
    public void DestroyEntity()
    {
        var context = new Context();

        var pool = context.entities;

        var entity = pool.Create();

        Assert.IsTrue(pool.ContainsId(entity.Id));

        entity.Add<PositionComponent>();
        entity.Add<MoveComponent>();

        var posPool = context.services.pool.Provide<PositionComponent>();
        var movePool = context.services.pool.Provide<MoveComponent>();

        Assert.AreEqual(posPool.Items.Count, 1);
        Assert.AreEqual(movePool.Items.Count, 1);

        entity.Destroy();

        Assert.IsFalse(pool.ContainsId(entity.Id));

        Assert.AreEqual(posPool.Items.Count, 0);
        Assert.AreEqual(movePool.Items.Count, 0);
    }
}