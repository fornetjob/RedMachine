using Assets.Scripts.Features.Resource;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesServiceMock : IResourcesService
{
    private Dictionary<string, string>
        _textsDict;

    public ResourcesServiceMock(Dictionary<string, string> textsDict)
    {
        _textsDict = textsDict;
    }

    public T ReadFrom<T>(string path) where T : Object
    {
        throw new System.NotImplementedException();
    }

    public string ReadStringFrom(string path)
    {
        return _textsDict[path];
    }
}