using UnityEngine;

namespace Assets.Scripts.Features.Resource
{
    public class ResourcesService: IResourcesService
    {
        #region Public methods

        public T ReadFrom<T>(string path)
            where T:UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public string ReadStringFrom(string path)
        {
            return ReadFrom<TextAsset>(path).text;
        }

        #endregion
    }
}