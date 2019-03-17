using Assets.Scripts.Features.Cameras;
using Assets.Scripts.Features.Board;
using UnityEngine;
using Assets.Scripts.Features.UI;
using Assets.Scripts.Features.Events;
using Assets.Scripts.Features.Bounces;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Times;

namespace Assets.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        #region Fields

        private Context
            _context;

        private ILateUpdateSystem
            _lateUpdateSystem;

        private IUpdateSystem
            _updateSystem;

        #endregion

        #region Game

        private void Start()
        {
            _context = new Context();

            _context.systems.Add(new TimeSystem());
            _context.systems.Add(new EventSystem());

            _context.systems.Add(new UISystem());
            _context.systems.Add(new CameraSystem());
            _context.systems.Add(new BoardSystem());

            _context.systems.Add(new BounceSystem());
            _context.systems.Add(new PoolSystem());

            _lateUpdateSystem = _context.systems;
            _updateSystem = _context.systems;
        }

        private void Update()
        {
            _updateSystem.OnUpdate();

            _lateUpdateSystem.OnLateUpdate();
        }

        #endregion
    }
}