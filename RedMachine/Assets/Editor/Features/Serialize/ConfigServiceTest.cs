﻿using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Resource;

using NUnit.Framework;

using System.Collections.Generic;

public class ConfigServiceTest
{
    [Test]
    public void ReadGameConfig()
    {
        string text = @"{""gameAreaWidth"":50,""gameAreaHeight"":50,""unitSpawnDelay"":500,""numUnitsToSpawn"":50,""minUnitRadius"":0.5,""maxUnitRadius"":1,""minUnitSpeed"":5.0,""maxUnitSpeed"":10.0}";

        var context = new Context(new ResourcesServiceMock(new Dictionary<string, string>
        {
            { ResourcesAssets.Configs_data, text }
        }));

        var config = context.services.serialize.GetGameConfig();

        Assert.AreEqual(config.gameAreaWidth, 50);
        Assert.AreEqual(config.gameAreaHeight, 50);
        Assert.AreEqual(config.unitSpawnDelay, 500);
        Assert.AreEqual(config.numUnitsToSpawn, 50);
        Assert.AreEqual(config.minUnitRadius, 0.5f);
        Assert.AreEqual(config.maxUnitRadius, 1);
        Assert.AreEqual(config.minUnitSpeed, 5f);
        Assert.AreEqual(config.maxUnitSpeed, 10f);

        context = new Context();

        config = context.services.serialize.GetGameConfig();

        Assert.IsNotNull(config);
    }
}