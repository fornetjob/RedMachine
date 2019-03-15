namespace Assets.Scripts.Features.Resource
{
    public interface IResourcesService : IService
    {
        string ReadStringFrom(string path);
        T ReadResourceFrom<T>(string path) 
            where T : UnityEngine.Object;
    }
}
