using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Times
{
    [RequireComponent(typeof(Slider))]
    public class TimeScaleView : ViewBase, IListener<TimeScaleComponent>
    {
        #region Constants

        public const string TimeScaleChanged = "TimeScaleView_TimeScaleChanged";

        #endregion

        #region Bindings

        [SerializeField]
        private Slider 
            _slider = null;

        [SerializeField]
        private Text 
            _valueText = null;

        #endregion

        #region Overriden methods

        protected override void OnBegin()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        #endregion

        #region Event handlers

        private void OnSliderValueChanged(float value)
        {
            _eventPool.Create()
                .Set(TimeScaleChanged, value);
        }

        #endregion

        #region IListener<TimeScaleComponent>

        void IListener<TimeScaleComponent>.OnChanged(TimeScaleComponent newValue)
        {
            _slider.value = newValue.Scale;

            _valueText.text = string.Format("{0:N2}x", newValue.Scale);
        }

        #endregion
    }
}