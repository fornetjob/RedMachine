using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Object;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Serialize;
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

        private GameConfig
            _gameConfig;

        #endregion

        #region ctor

        public UnitFactory(Context context)
        {
            _context = context;

            _entityPool = context.entities;
            _view = _context.services.view;

            _sprites = new WeakDictionary<string, Sprite>(path => _context.services.resources.ReadFrom<Sprite>(path));

            _gameConfig = _context.services.serialize.GetGameConfig();
        }

        #endregion

        #region Public methods

        public Entity Create(UnitType type, Vector2 pos, float radius, MoveComponent move = null)
        {
            var unitObj = new GameObject("Unit");

            Entity entity = _entityPool.Create();

            entity.AddListener(_view.Add<PositionView>(unitObj))
                .AddListener(_view.Add<SpriteRendererView>(unitObj))
                .AddListener(_view.Add<SpriteRadiusView>(unitObj));

            entity.Add<UnitComponent>().type = type;
            entity.Add<GameObjectComponent>().obj = unitObj;
            entity.Add<PositionComponent>().Position = pos;
            entity.Add<RadiusComponent>().Radius = radius;
            entity.Add<BoardSqueezeBoundComponent>().bound = _gameConfig.GetBoardSqueezeRadius(radius);

            var spritePath = type == UnitType.Red ? ResourcesAssets.Sprites_unitRed : ResourcesAssets.Sprites_unitBlue;

            entity.Add<SpriteComponent>()
                .Set(_sprites[spritePath]);

            if (move != null)
            {
                entity.Add<MoveComponent>().Set(move.moveDirection, move.speed);
            }

            return entity;
        }

        #endregion
    }
}