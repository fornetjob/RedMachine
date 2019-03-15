using Assets.Scripts.Features.Cameras;
using Assets.Scripts.Features.Configs;
using Assets.Scripts.Features.Board;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        private IFixedUpdateSystem
            _fixedSystem;

        private IUpdateSystem
            _updateSystem;

        private void Start()
        {
            var context = new Context();

            context.AddSystem(new CameraSystem());
            context.AddSystem(new BoardSystem());

            _fixedSystem = context;
            _updateSystem = context;
        }

        private void Update()
        {
            _updateSystem.OnUpdate();
        }

        private void FixedUpdate()
        {
            _fixedSystem.OnFixedUpdate();
        }
    }
}
