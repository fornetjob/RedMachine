using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;

using UnityEngine;

namespace Assets.Scripts.Features.Bounces
{
    public class BounceSystem : IAttachContext, IFixedUpdateSystem
    {
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

            _positions = _context.services.pool.Provide<PositionComponent>();
            _radiuses = _context.services.pool.Provide<RadiusComponent>();
            _moves = _context.services.pool.Provide<MoveComponent>();

            _units = _context.services.pool.Provide<UnitComponent>();

            _walls = _context.services.pool.Provide<WallComponent>();
        }

        public void OnFixedUpdate()
        {
            for (int unitIndex = 0; unitIndex < _units.Items.Count; unitIndex++)
            {
                var unit = _units.Items[unitIndex];
                var move = _moves.GetById(unit.id);
               
                var posItem = _positions.GetById(unit.id);
                var radius = _radiuses.GetById(unit.id).value.radius;

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

                    var otherPos = _positions.GetById(otherUnit.id).value.pos;
                    var otherRadius = _radiuses.GetById(otherUnit.id).value.radius;

                    if (Vector2.Distance(pos, otherPos) < radius + otherRadius)
                    {
                        var otherMove = _moves.GetById(otherUnit.id);

                        if (otherUnit.value.type == unit.value.type)
                        {
                            var direction = (pos - otherPos).normalized;

                            move.value.moveDirection = direction;
                            move.Set(move.value);

                            otherMove.value.moveDirection = direction * -1;
                            otherMove.Set(otherMove.value);
                        }
                    }
                }
            }
        }
    }
}