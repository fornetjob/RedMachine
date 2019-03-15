using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Times;
using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    public class MoveSystem : IAttachContext, IFixedUpdateSystem
    {
        #region Services

        private TimeService
            _time;

        #endregion

        //private Bounds
        //    _fieldBound;

        private ComponentPool<MoveComponent>
            _moves;

        private ComponentPool<PositionComponent>
            _positions;

        public void Attach(Context context)
        {
            _time = context.services.time;

            _moves = context.services.pool.Provide<MoveComponent>();
            _positions = context.services.pool.Provide<PositionComponent>();

            //_fieldBound = context.services.config.GetGameConfig().GetBoardBound();
        }

        public void OnFixedUpdate()
        {
            var time = _time.GetFixedDeltaTime();

            for (int moveIndex = 0; moveIndex < _moves.Items.Count; moveIndex++)
            {
                var moveItem = _moves.Items[moveIndex];
                var posItem = _positions.GetById(moveItem.id);

                var speed = moveItem.value.speed;

                posItem.value.pos += moveItem.value.moveDirection * speed * time;

                //if (_fieldBound.Contains(posItem.value.pos) == false)
                //{
                //    posItem.value.pos = _fieldBound.ClosestPoint(posItem.value.pos);
                //}

                posItem.Set(posItem.value);
            }
        }
    }
}
