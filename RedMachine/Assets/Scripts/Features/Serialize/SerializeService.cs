using System.Linq;

using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Features.Serialize
{
    public class SerializeService:IAttachContext, IService
    {
        #region Constants

        private const string GameSave = "GameSave";

        #endregion

        #region Services

        private PoolService
            _pool;

        #endregion

        #region Factories

        private UnitFactory
            _unitFactory;

        #endregion

        #region Fields

        private Context
            _context;

        private GameConfig
            _gameConfig;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _pool = _context.services.pool;

            _unitFactory = new UnitFactory(context);
        }

        #endregion

        #region Public methods

        public GameConfig GetGameConfig()
        {
            if (_gameConfig == null)
            {
                string text = _context.services.resources.ReadStringFrom(ResourcesAssets.Configs_data);

                _gameConfig = (GameConfig)JsonUtility.FromJson(text, typeof(GameConfig));
            }

            return _gameConfig;
        }

        public void SaveGame()
        {
            var info = new SaveInfo
            {
                State = _pool.Provide<BoardStateComponent>().Single(),
                Moves = _pool.Provide<MoveComponent>().Items.ToArray(),
                Positions = _pool.Provide<PositionComponent>().Items.ToArray(),
                Radiuses = _pool.Provide<RadiusComponent>().Items.ToArray(),
                Units = _pool.Provide<UnitComponent>().Items.ToArray()
            };

            PlayerPrefs.SetString(GameSave, JsonUtility.ToJson(info));
            PlayerPrefs.Save();
        }

        public bool LoadGame()
        {
            if (PlayerPrefs.HasKey(GameSave) == false)
            {
                return false;
            }

            var units = _pool.Provide<UnitComponent>().Items;

            while (units.Count > 0)
            {
                _context.entities.Destroy(units[0].Id);
            }

            var info = (SaveInfo)JsonUtility.FromJson(PlayerPrefs.GetString(GameSave), typeof(SaveInfo));

            _pool.Provide<BoardStateComponent>()
                .Single().Type = info.State.Type;

            var positionDict = info.Positions.ToDictionary(p => p.Id);
            var radiusDict = info.Radiuses.ToDictionary(p => p.Id);
            var moveDict = info.Moves.ToDictionary(p => p.Id);

            for (int i = 0; i < info.Units.Length; i++)
            {
                var unit = info.Units[i];

                var pos = positionDict[unit.Id].Position;
                var radius = radiusDict[unit.Id].Radius;

                MoveComponent move;

                moveDict.TryGetValue(unit.Id, out move);

                _unitFactory.Create(unit.type, pos, radius, move);
            }

            return true;
        }

        #endregion
    }
}