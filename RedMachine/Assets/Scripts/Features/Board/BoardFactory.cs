using Assets.Scripts.Features.Position;
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

            var boardEntity = _context.entities.Create()
                .AddListener(_view.Add<SpriteRendererView>(boardObj))
                .AddListener(_view.Add<SpriteSizeView>(boardObj));

            boardEntity.Add<BoardActionComponent>()
                .Type = BoardActionType.Add;

            boardEntity.Add<SpriteComponent>()
                .Set(_resources.ReadFrom<Sprite>(ResourcesAssets.Sprites_field), -1);

            boardEntity.Add<SizeComponent>()
                .Size = boardSize;

            var wallSize = boardSize + Vector2.one;

            var wallBeginPos = wallSize / 2f / -1;

            var beginPosX = wallBeginPos;
            beginPosX[0] = 0;

            var beginPosY = wallBeginPos;
            beginPosY[1] = 0;
            
            var sizeX = new Vector2(wallSize.x + 1, 1);
            var sizeY = new Vector2(1, wallSize.y - 1f);

            CreateWall(Vector2.up, beginPosX, sizeX);
            CreateWall(Vector2.right, beginPosY, sizeY);
            CreateWall(Vector2.down, beginPosX + new Vector2(0, wallSize.y), sizeX);
            CreateWall(Vector2.left, beginPosY + new Vector2(wallSize.x, 0), sizeY);

            return boardEntity;
        }

        #endregion

        #region Private methods

        private void CreateWall(Vector2 normal, Vector2 center, Vector2 size)
        {
            var wallObj = new GameObject("Wall");

            var wallEntity = _context.entities.Create()
                .AddListener(_view.Add<SpriteRendererView>(wallObj))
                .AddListener(_view.Add<PositionView>(wallObj))
                .AddListener(_view.Add<SpriteSizeView>(wallObj));

            wallEntity.Add<WallComponent>()
                .Set(normal, new Bounds(center, size));

            wallEntity.Add<SpriteComponent>()
                .Set(_resources.ReadFrom<Sprite>(ResourcesAssets.Sprites_wall));

            wallEntity.Add<PositionComponent>()
                .Position = center;

            wallEntity.Add<SizeComponent>()
                .Size = size;
        }

        #endregion
    }
}