using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Unit;
using NUnit.Framework;
using UnityEngine;

public class MoveSystemTest
{
    [Test]
    public void Move()
    {
        var context = new Context();

        var system = new MoveSystem();
        system.Attach(context);

        var entity = context.services.entityPool.NewEntity();

        entity.Add(new PositionComponent
        {
            pos = Vector2.zero
        });

        var moveDirection = Vector2.up;

        entity.Add(new MoveComponent
        {
            moveDirection = moveDirection,
            speed = 1
        });

        Assert.IsTrue(entity.IsExist<MoveComponent>());

        //int count = 0;

        //while (entity.IsExist<MoveComponent>()
        //    && count < 300000)
        //{
        //    system.OnFixedUpdate();

        //    count++;
        //}

        //Assert.IsFalse(entity.IsExist<MoveComponent>());

        //Assert.AreEqual(entity.Get<PositionComponent>().value.pos, moveTo);
    }
}