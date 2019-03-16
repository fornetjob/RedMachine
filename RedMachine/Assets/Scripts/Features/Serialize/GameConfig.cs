using System;
using UnityEngine;

namespace Assets.Scripts.Features.Serialize
{
    [Serializable]
    public class GameConfig
    {
        public int gameAreaWidth;
        public int gameAreaHeight;
        public int unitSpawnDelay;
        public int numUnitsToSpawn;
        public float minUnitRadius;
        public float maxUnitRadius;
        public float minUnitSpeed;
        public float maxUnitSpeed;

        public Vector2 GetBeginPos()
        {
            return GetBoardSize() / 2f / -1f;
        }

        public Vector2 GetBoardSize()
        {
            return new Vector2(gameAreaWidth, gameAreaHeight);
        }
    }
}