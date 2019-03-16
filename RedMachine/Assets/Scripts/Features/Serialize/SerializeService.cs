using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Resource;
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
                Identity = _context.services.pool.Provide<IdentityComponent>().Single(),
                Moves = _context.services.pool.Provide<MoveComponent>().Items.ToArray(),
                Positions = _context.services.pool.Provide<PositionComponent>().Items.ToArray(),
                Radiuses = _context.services.pool.Provide<RadiusComponent>().Items.ToArray(),
                Units = _context.services.pool.Provide<UnitComponent>().Items.ToArray()
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

            SceneManager.LoadScene("Play");

            var info = (SaveInfo)JsonUtility.FromJson(PlayerPrefs.GetString(GameSave), typeof(SaveInfo));

            Debug.Log(info.Identity.Id);

            return true;
        }

        #endregion
    }
}