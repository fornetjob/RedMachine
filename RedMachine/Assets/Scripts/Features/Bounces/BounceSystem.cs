using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Features.Bounces
{
    public class BounceSystem : IAttachContext, IFixedLateUpdateSystem
    {
        #region Service

        private ComponentPool<Entity>
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

        private List<int> 
            _entityToDestroyBuffer = new List<int>();

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _entityPool = _context.entities;

            _positions = _context.services.pool.Provide<PositionComponent>();
            _radiuses = _context.services.pool.Provide<RadiusComponent>();
            _moves = _context.services.pool.Provide<MoveComponent>();

            _units = _context.services.pool.Provide<UnitComponent>();

            _walls = _context.services.pool.Provide<WallComponent>();
        }

        #endregion

        #region IFixedLateUpdateSystem

        void IFixedLateUpdateSystem.OnFixedLateUpdate()
        {
            for (int unitIndex = 0; unitIndex < _units.Items.Count; unitIndex++)
            {
                var unit = _units.Items[unitIndex];
                var move = _moves.GetById(unit.Id);
               
                var posItem = _positions.GetById(unit.Id);
                var radiusItem = _radiuses.GetById(unit.Id);

                var radius = radiusItem.Radius;

                var pos = posItem.Position;

                #region Check walls

                for (int wallIndex = 0; wallIndex < _walls.Items.Count; wallIndex++)
                {
                    var wall = _walls.Items[wallIndex];

                    var closestPoint = wall.bound.ClosestPoint(pos);

                    var dist = Vector2.Distance(closestPoint, pos);

                    if (dist < radius)
                    {
                        Vector2 dir = Vector2.Reflect(move.moveDirection, wall.normal);

                        move.moveDirection = dir;

                        posItem.Position += dir * (radius - dist);
                    }
                }

                #endregion

                #region Check other units

                for (int otherUnitIndex = unitIndex + 1; otherUnitIndex < _units.Items.Count; otherUnitIndex++)
                {
                    var otherUnit = _units.Items[otherUnitIndex];

                    var otherRadiusItem = _radiuses.GetById(otherUnit.Id);

                    var otherPos = _positions.GetById(otherUnit.Id).Position;
                    var otherRadius = otherRadiusItem.Radius;

                    var dist = Vector2.Distance(pos, otherPos);

                    if (dist < radius + otherRadius)
                    {
                        if (otherUnit.type == unit.type)
                        {
                            var otherMove = _moves.GetById(otherUnit.Id);

                            var direction = (pos - otherPos).normalized;

                            move.moveDirection = direction;
                            otherMove.moveDirection = direction * -1;
                        }
                        else
                        {
                            var distLack = (radius + otherRadius - dist) / 2f;

                            radiusItem.Radius -= distLack;
                            otherRadiusItem.Radius -= distLack;

                            if (radius < MinBallRadius
                                || otherRadius < MinBallRadius)
                            {
                                _entityToDestroyBuffer.Add(unit.Id);
                                _entityToDestroyBuffer.Add(otherUnit.Id);
                            }
                        }
                    }
                }

                #endregion
            }

            #region Destroy units

            for (int i = 0; i < _entityToDestroyBuffer.Count;i++)
            {
                var entityId = _entityToDestroyBuffer[i];

                if (_entityPool.ContainsId(entityId))
                {
                    _entityPool.GetById(entityId).Destroy();
                }
            }

            _entityToDestroyBuffer.Clear();

            #endregion
        }

        #endregion
    }
}