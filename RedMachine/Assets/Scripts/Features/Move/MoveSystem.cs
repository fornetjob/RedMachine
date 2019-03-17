using Assets.Scripts.Features.Position;

namespace Assets.Scripts.Features.Move
{
    public class MoveSystem : IStartSystem, IUpdateSystem
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

        #region IStartSystem

        void IStartSystem.OnStart(Context context)
        {
            _context = context;

            _fixedTime = _context.services.time.GetFixedDeltaTime();

            _moves = context.services.pool.Provide<MoveComponent>();
            _positions = context.services.pool.Provide<PositionComponent>();
        }

        #endregion

        #region IUpdateSystem

        public void OnUpdate()
        {
            var time = _context.services.time.GetDeltaTime();

            for (int moveIndex = 0; moveIndex < _moves.Items.Count; moveIndex++)
            {
                var moveItem = _moves.Items[moveIndex];
                var posItem = _positions.GetById(moveItem.Id);

                var speed = moveItem.speed;

                posItem.Position += moveItem.moveDirection * speed * time;
            }
        }

        #endregion
    }
}