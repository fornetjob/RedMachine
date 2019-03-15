using Assets.Scripts.Features.Configs;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Sprites;
using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class BoardFactory
    {
        private Context
            _context;

        public BoardFactory(Context context)
        {
            _context = context;
        }

        public Entity Create(GameConfig config)
        {
            var boardEntity = _context.services.entityPool.NewEntity();
            boardEntity.Add(new BoardActionComponent { type = BoardActionType.Add });

            var boardObj = new GameObject("Board");

            boardEntity.Add(new SpriteComponent
            {
                sprite = _context.services.resources.ReadResourceFrom<Sprite>(ResourcesAssets.Sprites_field),
                sortingOrder = -1
            }).AddListener(boardObj.AddView<SpriteRendererView>());

            var boardSize = config.GetBoardSize();

            boardEntity.Add(new SpriteSizeComponent { size = boardSize })
                .AddListener(boardObj.AddView<SpriteScaleView>());

            var wallSize = boardSize + Vector2.one;

            var wallBeginPos = wallSize / 2f / -1;

            var beginPosX = wallBeginPos;
            beginPosX[0] = 0;

            var beginPosY = wallBeginPos;
            beginPosY[1] = 0;
            
            var sizeX = new Vector2(wallSize.x + 1, 1);
            var sizeY = new Vector2(1, wallSize.y - 1f);

            CreateWall(beginPosX, sizeX);
            CreateWall(beginPosY, sizeY);
            CreateWall(beginPosX + new Vector2(0, wallSize.y), sizeX);
            CreateWall(beginPosY + new Vector2(wallSize.x, 0), sizeY);

            return boardEntity;
        }

        private void CreateWall(Vector2 center, Vector2 size)
        {
            var wallObj = new GameObject("Wall");

            var wallEntity = _context.services.entityPool.NewEntity();

            wallEntity.Add(new SpriteComponent
            {
                sprite = _context.services.resources.ReadResourceFrom<Sprite>(ResourcesAssets.Sprites_wall),
            }).AddListener(wallObj.AddView<SpriteRendererView>());

            wallEntity.Add(new PositionComponent { pos = center })
                .AddListener(wallObj.AddView<PositionView>());

            wallEntity.Add(new SpriteSizeComponent { size = size })
                .AddListener(wallObj.AddView<SpriteScaleView>());
        }
    }
}
