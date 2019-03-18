using Assets.Scripts.Features.Scale;   
using Assets.Scripts.Features.Sprites;
using Assets.Scripts.Features.Views;
using Assets.Scripts.Features.Resource;

using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class BoardFactory
    {
        #region Services

        private readonly ViewService
            _view;

        private readonly IResourcesService
            _resources;

        #endregion

        #region Fields

        private readonly Context
            _context;

        #endregion

        #region ctor

        public BoardFactory(Context context)
        {
            _context = context;

            _view = _context.services.view;
            _resources = _context.services.resources;
        }

        #endregion

        #region Public methods

        public Entity Create(Vector2 boardSize)
        {
            var boardObj = new GameObject("Board");

            Entity boardEntity = _context.entities.Create();

            boardEntity.Add<BoardStateComponent>()
                .Type = BoardStateType.Add;

            boardEntity.AddListener(_view.Add<SpriteRendererView>(boardObj))
                .AddListener(_view.Add<SpriteSizeView>(boardObj));

            boardEntity.Add<SpriteComponent>()
                .Set(_resources.ReadFrom<Sprite>(ResourcesAssets.Sprites_field), -1);

            boardEntity.Add<SizeComponent>()
                .Size = boardSize;

            return boardEntity;
        }

        #endregion
    }
}