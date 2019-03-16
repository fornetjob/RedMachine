using Assets.Scripts.Features.Serialize;
using UnityEngine;

namespace Assets.Scripts.Features.Cameras
{
    public class CameraSystem : IAttachContext, IStartSystem, IUpdateSystem
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

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _resizeCameraTick = context.services.time.WaitTo(1, true);
            _config = context.services.serialize.GetGameConfig();
        }

        #endregion

        #region IStartSystem

        void IStartSystem.OnStart()
        {
            _camera = Camera.main;

            ResizeCamera();
        }

        #endregion

        #region IUpdateSystem

        void IUpdateSystem.OnUpdate()
        {
            if (_resizeCameraTick.IsCheck()
               && _aspect != _camera.aspect)
            {
                ResizeCamera();
            }
        }

        #endregion

        #region Private methods

        private void ResizeCamera()
        {
            var size = _config.GetBoardSize() + Vector2.one * _config.maxUnitRadius * 2f + Vector2.one * 10;

            size[0] /= _camera.aspect;

            size /= 2f;

            _camera.orthographicSize = Mathf.Max(size.x, size.y);

            _aspect = _camera.aspect;
        }

        #endregion
    }
}