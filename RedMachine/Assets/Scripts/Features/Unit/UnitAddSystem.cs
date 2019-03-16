using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Randoms;

using UnityEngine;

namespace Assets.Scripts.Features.Unit
{
    public class UnitAddSystem : IAttachContext, IStartSystem, IUpdateSystem, ISystem
    {
        #region Services

        private RandomService
            _random;

        #endregion

        #region Factories

        private UnitFactory
            _playerFactory;

        #endregion

        #region Fields

        private ComponentPool<UnitComponent>
            _units;

        private Context
            _context;

        private Wait
            _tick;

        private GameConfig
            _gameConfig;

        private Vector2
            _beginPos;

        private Vector2
            _maxBoardPos;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _random = _context.services.random;
            
            _playerFactory = new UnitFactory(context);

            _units = _context.services.pool.Provide<UnitComponent>();
        }

        #endregion

        #region IStartSystem

        void IStartSystem.OnStart()
        {
            _gameConfig = _context.services.serialize.GetGameConfig();
            _tick = _context.services.time.WaitTo(_gameConfig.unitSpawnDelay / 1000f, true);

            var unitRadiusOffset = Vector2.one * _gameConfig.maxUnitRadius;

            _beginPos = _gameConfig.GetBeginPos() + unitRadiusOffset;

            _maxBoardPos = _beginPos + _gameConfig.GetBoardSize() - unitRadiusOffset * 2;
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            if (_units.Items.Count < _gameConfig.numUnitsToSpawn)
            {
                if (_tick.IsCheck())
                {
                    var radius = _random.Range(_gameConfig.minUnitRadius, _gameConfig.maxUnitRadius);

                    var pos = _beginPos + new Vector2(
                        _random.Range(0, _gameConfig.gameAreaWidth), 
                        _random.Range(0, _gameConfig.gameAreaHeight));

                    pos[0] = pos.x + radius > _maxBoardPos.x ? _maxBoardPos.x : pos.x;
                    pos[1] = pos.y + radius > _maxBoardPos.y ? _maxBoardPos.y : pos.y;

                    UnitType type = _random.RandomBool() ? UnitType.Red : UnitType.Blue;

                    _playerFactory.Create(type, pos, radius);
                }
            }
            else
            {
                _context.systems.Remove(this);

                var entityPool = _context.entities;

                for (int i = 0; i < _units.Items.Count; i++)
                {
                    var unit = _units.Items[i];

                    var entity = entityPool.GetById(unit.Id);

                    var radian = _random.Range(0, 360) * Mathf.Deg2Rad;

                    entity.Add<MoveComponent>()
                        .Set(new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)),
                            _random.Range(_gameConfig.minUnitSpeed, _gameConfig.maxUnitSpeed));
                }

                _context.services.pool.Provide<BoardActionComponent>().Single()
                    .Type = BoardActionType.Move;
            }
        }

        #endregion
    }
}