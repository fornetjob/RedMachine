using UnityEngine;

namespace Assets.Scripts.Features.Views
{
    public class ViewService:IService, IAttachContext
    {
        #region Private methods

        private ComponentPool<EventComponent>
            _eventPool;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _eventPool = context.services.pool.Provide<EventComponent>();
        }

        #endregion

        #region Public methods

        public TView Add<TView>(GameObject obj)
            where TView : ViewBase
        {
            var view = obj.AddComponent<TView>();

            BeginView(view);

            return view;
        }

        public TView[] AttachChildren<TView>(string path)
            where TView : ViewBase
        {
            var children = GameObject.Find(path).GetComponentsInChildren<TView>();

            for (int i = 0; i < children.Length; i++)
            {
                BeginView(children[i]);
            }

            return children;
        }

        public TView Attach<TView>(string path)
            where TView : ViewBase
        {
            var view = GameObject.Find(path).GetComponent<TView>();

            BeginView(view);

            return view;
        }

        #endregion

        #region Private methods

        private void BeginView<TView>(TView view)
            where TView : ViewBase
        {
            view.BeginView(_eventPool);
        }

        #endregion
    }
}