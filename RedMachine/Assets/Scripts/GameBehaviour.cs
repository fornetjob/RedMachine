using Assets.Scripts.Features.Cameras;
using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Board;
using UnityEngine;
using Assets.Scripts.Features.UI;

namespace Assets.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        #region Fields

        private Context
            _context;

        private IFixedLateUpdateSystem
            _fixedLateUpdateSystem;

        private IFixedUpdateSystem
            _fixedupdateSystem;

        private IUpdateSystem
            _updateSystem;

        #endregion

        #region Game

        private void Start()
        {
            _context = new Context();

            _context.systems.Add(new UISystem());
            _context.systems.Add(new CameraSystem());
            _context.systems.Add(new BoardSystem());

            _fixedLateUpdateSystem = _context.systems;
            _fixedupdateSystem = _context.systems;
            _updateSystem = _context.systems;
        }

        private void Update()
        {
            _updateSystem.OnUpdate();
        }

        private void FixedUpdate()
        {
            _fixedupdateSystem.OnFixedUpdate();
            _fixedLateUpdateSystem.OnFixedLateUpdate();
        }

        #endregion
    }
}