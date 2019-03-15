using Assets.Scripts.Features.Object;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Sprites;
using UnityEngine;

namespace Assets.Scripts.Features.Unit
{
    public class UnitFactory
    {
        #region Services

        private EntityPool
            _entityPool;

        #endregion

        private Context
            _context;

        private readonly WeakDictionary<string, Sprite>
            _sprites;

        public UnitFactory(Context context)
        {
            _context = context;

            _entityPool = context.entities;

            _sprites = new WeakDictionary<string, Sprite>(path => _context.services.resources.ReadResourceFrom<Sprite>(path));
        }

        public Entity Create(UnitType type, Vector2 pos, float radius)
        {
            var entity = _entityPool.NewEntity();

            var unitObj = new GameObject(string.Format("Unit_{0}", entity.id));

            entity.Add(new UnitComponent { type = type });
            entity.Add(new GameObjectComponent { obj = unitObj });

            entity.Add(new PositionComponent{ pos = pos})
                .AddListener(unitObj.AddView<PositionView>());

            var sprite = _sprites[type == UnitType.Red ? ResourcesAssets.Sprites_unitRed : ResourcesAssets.Sprites_unitBlue];

            entity.Add(new SpriteComponent { sprite = sprite })
                .AddListener(unitObj.AddView<SpriteRendererView>());

            entity.Add(new RadiusComponent { radius = radius })
                .AddListener(unitObj.AddView<SpriteScaleView>());

            return entity;
        }
    }
}