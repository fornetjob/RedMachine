using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Randoms;

using UnityEngine;

namespace Assets.Scripts.Features.Unit
{
    public class UnitAddSystem : IStartSystem, IListener<WaitComponent>
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

        private GameConfig
            _gameConfig;

        private Vector2
            _beginPos;

        private Vector2
            _maxBoardPos;

        private Entity
            _waitEntity;

        #endregion

        #region IStartSystem

        void IStartSystem.OnStart(Context context)
        {
            _context = context;

            _random = _context.services.random;

            _playerFactory = new UnitFactory(context);

            _units = _context.services.pool.Provide<UnitComponent>();

            _gameConfig = _context.services.serialize.GetGameConfig();

            var unitRadiusOffset = Vector2.one * _gameConfig.maxUnitRadius;

            _beginPos = _gameConfig.GetBeginPos() + unitRadiusOffset;

            _maxBoardPos = _beginPos + _gameConfig.GetBoardSize() - unitRadiusOffset * 2;

            _waitEntity = _context.entities.Create()
                .AddListener(this);

            _waitEntity.Add<WaitComponent>()
                .Set(_gameConfig.unitSpawnDelay / 1000f, true);
        }

        #endregion

        #region IListener<WaitComponent>

        void IListener<WaitComponent>.OnChanged(WaitComponent value)
        {
            if (_units.Items.Count < _gameConfig.numUnitsToSpawn)
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
            else
            {
                _context.systems.Remove(this);
                _waitEntity.RemoveListener(this); 

                var entityPool = _context.entities;

                for (int i = 0; i < _units.Items.Count; i++)
                {
                    var unit = _units.Items[i];

                    var entity = entityPool.GetById(unit.Id);

                    var radian = _random.Range(0, 360) * Mathf.Deg2Rad;

                    entity.Add<MoveComponent>()
                        .Set(new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized,
                            _random.Range(_gameConfig.minUnitSpeed, _gameConfig.maxUnitSpeed));
                }

                _context.services.pool.Provide<BoardActionComponent>().Single()
                    .Type = BoardActionType.Move;
            }
        }

        #endregion
    }
}