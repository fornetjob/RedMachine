using Assets.Scripts.Features.Cameras;
using Assets.Scripts.Features.Board;
using UnityEngine;
using Assets.Scripts.Features.UI;
using Assets.Scripts.Features.Events;
using Assets.Scripts.Features.Bounces;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Times;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Unit;

namespace Assets.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        #region Fields

        private Context
            _context;

        private IUpdateSystem
            _updateSystem;

        #endregion

        #region Game

        private void Start()
        {
            _context = new Context();

            _context.systems.Add(new TimeSystem());

            _context.systems.Add(new CameraSystem());

            _context.systems.Add(new BoardSystem());
            _context.systems.Add(new UISystem());

            _context.systems.Add(new UnitAddSystem());
            _context.systems.Add(new MoveSystem());

            _context.systems.Add(new BounceSystem().SetOrder(int.MaxValue - 2));
            _context.systems.Add(new EventSystem().SetOrder(int.MaxValue - 1));
            _context.systems.Add(new PoolSystem().SetOrder(int.MaxValue));

            _updateSystem = _context.systems;
        }

        private void Update()
        {
            _updateSystem.OnUpdate();
        }

        #endregion
    }
}