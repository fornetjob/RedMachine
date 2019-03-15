using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Times;

namespace Assets.Scripts.Features.Move
{
    public class MoveSystem : IAttachContext, IFixedUpdateSystem
    {
        #region Services

        private TimeService
            _time;

        #endregion

        private ComponentPool<MoveComponent>
            _moves;

        private ComponentPool<WallComponent>
            _walls;

        private ComponentPool<PositionComponent>
            _positions;

        public void Attach(Context context)
        {
            _time = context.services.time;

            _moves = context.services.pool.Provide<MoveComponent>();
            _walls = context.services.pool.Provide<WallComponent>();
            _positions = context.services.pool.Provide<PositionComponent>();
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

                //var radiusPos = posItem.value.pos + moveItem.value.moveDirection * 

                //for (int wallIndex = 0; wallIndex < _walls.Items.Count; wallIndex++)
                //{
                //    var wall = _walls.Items[wallIndex];

                //    if (wall.value.bound.Contains(posItem.value.pos))
                //    {

                //    }
                //}

                //if (Vector3.Distance(posItem.value.pos, moveItem.value.moveDirection) < Mathf.Epsilon)
                //{
                //    posItem.value.pos = moveItem.value.moveDirection;
                    
                //    moveItem.Destroy();
                //    i--;
                //}

                posItem.Set(posItem.value);
            }
        }
    }
}
