using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Unit;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Features.Bounces
{
    public class BounceSystem : IStartSystem, ILateUpdateSystem
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

        private ComponentPool<BoardSqueezeBoundComponent>
            _bounds;

        private ComponentPool<UnitComponent>
            _units;

        private List<int> 
            _entityToDestroyBuffer = new List<int>();

        private GameConfig
            _gameConfig;

        #endregion

        #region Properties

        public bool IsEnabled;

        #endregion

        #region IStartSystem

        void IStartSystem.OnStart(Context context)
        {
            _context = context;

            _entityPool = _context.entities;

            _positions = _context.services.pool.Provide<PositionComponent>();
            _radiuses = _context.services.pool.Provide<RadiusComponent>();
            _moves = _context.services.pool.Provide<MoveComponent>();
            _bounds = _context.services.pool.Provide<BoardSqueezeBoundComponent>();

            _units = _context.services.pool.Provide<UnitComponent>();

            _gameConfig = _context.services.serialize.GetGameConfig();
        }

        #endregion

        #region ILateUpdateSystem

        void ILateUpdateSystem.OnLateUpdate()
        {
            if (IsEnabled == false)
            {
                return;
            }

            CheckUnitsBounces();

            DestroyUnits();

            CheckWallsBounces();
        }

        #endregion

        #region Private methods

        private void CheckUnitsBounces()
        {
            for (int unitIndex = 0; unitIndex < _units.Items.Count; unitIndex++)
            {
                var unitItem = _units.Items[unitIndex];
                var moveItem = _moves.GetById(unitItem.Id);
                var posItem = _positions.GetById(unitItem.Id);
                var radiusItem = _radiuses.GetById(unitItem.Id);

                var radius = radiusItem.Radius;
                var pos = posItem.Position;

                for (int otherUnitIndex = unitIndex + 1; otherUnitIndex < _units.Items.Count; otherUnitIndex++)
                {
                    var otherUnit = _units.Items[otherUnitIndex];

                    var otherRadiusItem = _radiuses.GetById(otherUnit.Id);

                    var otherPos = _positions.GetById(otherUnit.Id).Position;
                    var otherRadius = otherRadiusItem.Radius;

                    var distBetweenBalls = Vector2.Distance(pos, otherPos);

                    if (distBetweenBalls < radius + otherRadius)
                    {
                        if (otherUnit.type == unitItem.type)
                        {
                            var otherMove = _moves.GetById(otherUnit.Id);

                            var direction = (pos - otherPos).normalized;

                            moveItem.moveDirection = direction;
                            otherMove.moveDirection = direction * -1;
                        }
                        //else
                        //{
                        //    var distLack = (radius + otherRadius - distBetweenBalls) / 2f;

                        //    radiusItem.Radius -= distLack;
                        //    otherRadiusItem.Radius -= distLack;

                        //    if (radiusItem.Radius < MinBallRadius
                        //        || otherRadiusItem.Radius < MinBallRadius)
                        //    {
                        //        _entityToDestroyBuffer.Add(unit.Id);
                        //        _entityToDestroyBuffer.Add(otherUnit.Id);
                        //    }
                        //    else
                        //    {
                        //        bounceBoundItem.bound = _gameConfig.GetBoardSqueezeRadius(radiusItem.Radius);
                        //        _bounds.GetById(otherUnit.Id).bound = _gameConfig.GetBoardSqueezeRadius(otherRadiusItem.Radius);
                        //    }
                        //}
                    }
                }
            }
        }

        private void CheckWallsBounces()
        {
            for (int unitIndex = 0; unitIndex < _units.Items.Count; unitIndex++)
            {
                var unit = _units.Items[unitIndex];

                var move = _moves.GetById(unit.Id);
                var posItem = _positions.GetById(unit.Id);

                var pos = posItem.Position;

                var bounceBoundItem = _bounds.GetById(unit.Id);

                if (bounceBoundItem.bound.Contains(pos) == false)
                {
                    var closestPos = (Vector2)bounceBoundItem.bound.ClosestPoint(pos);

                    var normal = (pos - closestPos).normalized;

                    posItem.Position = closestPos;

                    Vector2 reflectionDir = Vector2.Reflect(move.moveDirection, normal);

                    move.moveDirection = reflectionDir;
                }
            }
        }

        private void DestroyUnits()
        {
            for (int i = 0; i < _entityToDestroyBuffer.Count; i++)
            {
                var entityId = _entityToDestroyBuffer[i];

                if (_entityPool.ContainsId(entityId))
                {
                    _entityPool.GetById(entityId).Destroy();
                }
            }

            _entityToDestroyBuffer.Clear();
        }

        #endregion
    }
}