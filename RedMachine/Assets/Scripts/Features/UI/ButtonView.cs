using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonView:ViewBase
    {
        #region Constants

        public const string ButtonClick = "ButtonView_ButtonClick";

        #endregion

        #region Bindings

        private Button
            _btn;

        #endregion

        #region Properties

        public ButtonActionType Type;

        #endregion

        #region Overriden methods

        protected override void OnBegin()
        {
            _btn = gameObject.GetComponent<Button>();
            _btn.onClick.AddListener(OnButtonClick);
        }

        #endregion

        #region Event handlers

        void OnButtonClick()
        {
            _eventPool.Create()
                .Set(ButtonClick, Type);
        }

        #endregion
    }
}