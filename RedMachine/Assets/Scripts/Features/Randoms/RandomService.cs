using UnityEngine;

namespace Assets.Scripts.Features.Randoms
{
    public class RandomService:IService
    {
        public bool RandomBool()
        {
            return Range(0, 2) == 0;
        }

        public int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}