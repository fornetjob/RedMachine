namespace Assets.Scripts.Features.Resource
{
    public interface IResourcesService : IService
    {
        string ReadStringFrom(string path);
        T ReadFrom<T>(string path) 
            where T : UnityEngine.Object;
    }
}
