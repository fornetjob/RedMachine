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

        var entity = context.entities.Create();

        entity.Add<PositionComponent>()
            .Position = Vector2.zero;

        var moveDirection = Vector2.up;

        entity.Add<MoveComponent>()
            .Set(moveDirection, 1);

        Assert.IsTrue(entity.IsExist<MoveComponent>());
    }
}