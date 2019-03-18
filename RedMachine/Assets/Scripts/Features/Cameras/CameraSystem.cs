using Assets.Scripts.Features.Serialize;
using UnityEngine;

namespace Assets.Scripts.Features.Cameras
{
    public class CameraSystem : SystemBase, IBeginSystem, IListener<WaitComponent>
    {
        #region Fields

        private GameConfig
            _config;

        private Camera
            _camera;

        private float
            _aspect;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _config = context.services.serialize.GetGameConfig();

            _camera = Camera.main;

            ResizeCamera();

            context.entities.Create()
                .AddListener(this)
                .Add<WaitComponent>()
                .Set(1, true);
        }

        #endregion

        #region IListener<WaitComponent>

        public void OnChanged(WaitComponent value)
        {
            if (_aspect != _camera.aspect)
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