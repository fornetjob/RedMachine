using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Position;
using Assets.Scripts.Features.Scale;
using Assets.Scripts.Features.Unit;

using System;

namespace Assets.Scripts.Features.Serialize
{
    [Serializable]
    public class SaveInfo
    {
        public BoardStateComponent State;
        public UnitComponent[] Units;
        public PositionComponent[] Positions;
        public RadiusComponent[] Radiuses;
        public MoveComponent[] Moves;
    }
}
