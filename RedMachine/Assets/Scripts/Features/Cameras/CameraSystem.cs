using Assets.Scripts.Features.Configs;
using UnityEngine;

namespace Assets.Scripts.Features.Cameras
{
    public class CameraSystem : IAttachContext, IUpdateSystem
    {
        #region Fields

        private GameConfig
            _config;

        private Camera
            _camera;

        private float
            _aspect;

        private Wait
            _resizeCameraTick;

        #endregion

        public void Attach(Context context)
        {
            _resizeCameraTick = context.services.time.WaitTo(1, true);
            _config = context.services.config.GetGameConfig();

            _camera = Camera.main;

            ResizeCamera();
        }

        public void OnUpdate()
        {
            if (_resizeCameraTick.IsCheck()
               && _aspect != _camera.aspect)
            {
                ResizeCamera();
            }
        }

        private void ResizeCamera()
        {
            var size = _config.GetBoardSize() + Vector2.one * _config.maxUnitRadius * 2f;

            size[0] /= _camera.aspect;

            size /= 2f;

            _camera.orthographicSize = Mathf.Max(size.x, size.y);

            _aspect = _camera.aspect;
        }
    }
}
