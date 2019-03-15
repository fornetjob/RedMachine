using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Features.Bounces
{
    public class BounceSystem : IAttachContext, IFixedUpdateSystem
    {
        #region Service

        private EntityPool
            _entityPool;

        #endregion

        #region Constants

        private const float MinBallRadius = 0.2f;

        #endregion

        #region Fields

        private Context
            _context;

        private ComponentPool<PositionComponent>
            _positions;

        private ComponentPool<RadiusComponent>
            _radiuses;

        private ComponentPool<MoveComponent>
            _moves;

        private ComponentPool<WallComponent>
            _walls;

        private ComponentPool<UnitComponent>
            _units;

        #endregion

        public void Attach(Context context)
        {
            _context = context;

            _entityPool = _context.entities;

            _positions = _context.services.pool.Provide<PositionComponent>();
            _radiuses = _context.services.pool.Provide<RadiusComponent>();
            _moves = _context.services.pool.Provide<MoveComponent>();

            _units = _context.services.pool.Provide<UnitComponent>();

            _walls = _context.services.pool.Provide<WallComponent>();
        }

        public void OnFixedUpdate()
        {
            List<int> entityToDestroy = new List<int>();

            for (int unitIndex = 0; unitIndex < _units.Items.Count; unitIndex++)
            {
                var unit = _units.Items[unitIndex];
                var move = _moves.GetById(unit.id);
               
                var posItem = _positions.GetById(unit.id);
                var radiusItem = _radiuses.GetById(unit.id);

                var radius = radiusItem.value.radius;

                var pos = posItem.value.pos;

                for (int wallIndex = 0; wallIndex < _walls.Items.Count; wallIndex++)
                {
                    var wall = _walls.Items[wallIndex];

                    var closestPoint = wall.value.bound.ClosestPoint(pos);

                    var dist = Vector2.Distance(closestPoint, pos);

                    if (dist < radius)
                    {
                        Vector2 dir = Vector2.Reflect(move.value.moveDirection, wall.value.normal);

                        move.value.moveDirection = dir;
                        move.Set(move.value);

                        posItem.value.pos += dir * (radius - dist);
                        posItem.Set(posItem.value);
                    }
                }

                for (int otherUnitIndex = unitIndex + 1; otherUnitIndex < _units.Items.Count; otherUnitIndex++)
                {
                    var otherUnit = _units.Items[otherUnitIndex];

                    var otherRadiusItem = _radiuses.GetById(otherUnit.id);

                    var otherPos = _positions.GetById(otherUnit.id).value.pos;
                    var otherRadius = otherRadiusItem.value.radius;

                    var dist = Vector2.Distance(pos, otherPos);

                    if (dist < radius + otherRadius)
                    {
                        if (otherUnit.value.type == unit.value.type)
                        {
                            var otherMove = _moves.GetById(otherUnit.id);

                            var direction = (pos - otherPos).normalized;

                            move.value.moveDirection = direction;
                            move.Set(move.value);

                            otherMove.value.moveDirection = direction * -1;
                            otherMove.Set(otherMove.value);
                        }
                        else
                        {
                            var distLack = (radius + otherRadius - dist) / 2f;

                            radiusItem.value.radius -= distLack;
                            otherRadiusItem.value.radius -= distLack;

                            if (radius < MinBallRadius
                                || otherRadius < MinBallRadius)
                            {
                                entityToDestroy.Add(unit.id);
                                entityToDestroy.Add(otherUnit.id);
                            }
                            else
                            {
                                radiusItem.Set(radiusItem.value);
                                otherRadiusItem.Set(otherRadiusItem.value);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < entityToDestroy.Count;i++)
            {
                var entityId = entityToDestroy[i];

                if (_entityPool.ContainsId(entityId))
                {
                    _entityPool.GetById(entityId).Destroy();
                }
            }
        }
    }
}