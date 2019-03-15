using NUnit.Framework;

public class IdentitiesTest
{
    [Test]
    public void CreateEntity()
    {
        var context = new Context();

        var entity = context.services.entityPool.NewEntity();

        Assert.AreEqual(entity.id, 1);

        Assert.AreEqual(context.services.identity.GetId(), 1);

        context.services.entityPool.NewEntity();

        Assert.AreEqual(context.services.identity.GetId(), 2);

        context.services.entityPool.NewEntity();
        context.services.entityPool.NewEntity();

        Assert.AreEqual(context.services.identity.GetId(), 4);
    }
}