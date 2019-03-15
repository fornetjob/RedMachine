using Assets.Scripts.Features.Resource;
using UnityEngine;

namespace Assets.Scripts.Features.Configs
{
    public class ConfigService:IService
    {
        #region Services

        private IResourcesService
            _resourcesService;

        #endregion

        #region Fields

        private GameConfig
            _gameConfig;

        #endregion

        public ConfigService(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        public GameConfig GetGameConfig()
        {
            if (_gameConfig == null)
            {
                string text = _resourcesService.ReadStringFrom(ResourcesAssets.Configs_data);

                _gameConfig = (GameConfig)JsonUtility.FromJson(text, typeof(GameConfig));
            }

            return _gameConfig;
        }
    }
}