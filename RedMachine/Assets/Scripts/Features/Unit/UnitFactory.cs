using Assets.Scripts.Features.Object;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Sprites;
using Assets.Scripts.Features.Views;
using UnityEngine;

namespace Assets.Scripts.Features.Unit
{
    public class UnitFactory
    {
        #region Services

        private ComponentPool<Entity>
            _entityPool;

        private ViewService
            _view;

        #endregion

        #region Fields

        private Context
            _context;

        private readonly WeakDictionary<string, Sprite>
            _sprites;

        #endregion

        #region ctor

        public UnitFactory(Context context)
        {
            _context = context;

            _entityPool = context.entities;
            _view = _context.services.view;

            _sprites = new WeakDictionary<string, Sprite>(path => _context.services.resources.ReadFrom<Sprite>(path));
        }

        #endregion

        #region Public methods

        public Entity Create(UnitType type, Vector2 pos, float radius)
        {
            var unitObj = new GameObject("Unit");

            var entity = _entityPool.Create()
                .AddListener(_view.Add<PositionView>(unitObj))
                .AddListener(_view.Add<SpriteRendererView>(unitObj))
                .AddListener(_view.Add<SpriteRadiusView>(unitObj));

            entity.Add<UnitComponent>().type = type;
            entity.Add<GameObjectComponent>().obj = unitObj;
            entity.Add<PositionComponent>().Position = pos;
            entity.Add<RadiusComponent>().Radius = radius;

            var spritePath = type == UnitType.Red ? ResourcesAssets.Sprites_unitRed : ResourcesAssets.Sprites_unitBlue;

            entity.Add<SpriteComponent>()
                .Set(_sprites[spritePath]);

            return entity;
        }

        #endregion
    }
}