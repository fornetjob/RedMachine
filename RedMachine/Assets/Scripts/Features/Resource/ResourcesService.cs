using UnityEngine;

namespace Assets.Scripts.Features.Resource
{
    public class ResourcesService: IResourcesService
    {
        public T ReadResourceFrom<T>(string path)
            where T:UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public string ReadStringFrom(string path)
        {
            return ReadResourceFrom<TextAsset>(path).text;
        }
    }
}