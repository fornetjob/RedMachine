using Assets.Scripts.Features.Position;

namespace Assets.Scripts.Features.Move
{
    public class MoveSystem : IAttachContext, IFixedUpdateSystem
    {
        #region Fields

        private Context
            _context;

        private ComponentPool<MoveComponent>
            _moves;

        private ComponentPool<PositionComponent>
            _positions;

        private float
            _fixedTime;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _fixedTime = _context.services.time.GetFixedDeltaTime();

            _moves = context.services.pool.Provide<MoveComponent>();
            _positions = context.services.pool.Provide<PositionComponent>();
        }

        #endregion

        #region IFixedUpdateSystem

        public void OnFixedUpdate()
        {
            for (int moveIndex = 0; moveIndex < _moves.Items.Count; moveIndex++)
            {
                var moveItem = _moves.Items[moveIndex];
                var posItem = _positions.GetById(moveItem.Id);

                var speed = moveItem.speed;

                posItem.Position += moveItem.moveDirection * speed * _fixedTime;
            }
        }

        #endregion
    }
}