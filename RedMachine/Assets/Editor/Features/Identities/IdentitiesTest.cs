using Assets.Scripts.Features.Identities;

using NUnit.Framework;

public class IdentitiesTest
{
    [Test]
    public void CreateEntity()
    {
        var context = new Context();

        var entity = context.entities.Create();

        Assert.AreEqual(entity.Id, 1);

        var idItem = context.services.pool.Provide<IdentityComponent>().Single();

        Assert.AreEqual(idItem.identity, 1);

        context.entities.Create();

        Assert.AreEqual(idItem.identity, 2);

        context.entities.Create();
        context.entities.Create();

        Assert.AreEqual(idItem.identity, 4);
    }
}