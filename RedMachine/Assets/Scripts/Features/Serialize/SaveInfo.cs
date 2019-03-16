using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;

using System;
using UnityEngine;

namespace Assets.Scripts.Features.Serialize
{
    [Serializable]
    public class SaveInfo
    {
        public IdentityComponent Identity;
        public UnitComponent[] Units;
        public PositionComponent[] Positions;
        public RadiusComponent[] Radiuses;
        public MoveComponent[] Moves;
    }
}
