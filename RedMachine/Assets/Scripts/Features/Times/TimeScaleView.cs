using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Times
{
    public class TimeScaleView : ViewBase, IComponentListener<TimeScaleComponent>
    {
        #region Bindings

        [SerializeField]
        private Slider 
            _slider = null;

        [SerializeField]
        private Text 
            _valueText = null;

        #endregion

        public UnityAction<float> OnSliderValueChanged;

        protected override void OnBegin()
        {
            _slider.onValueChanged.AddListener((value)=>OnSliderValueChanged(value));
        }

        public void OnChanged(TimeScaleComponent newValue)
        {
            _slider.value = newValue.scale;

            _valueText.text = string.Format("{0:N2}x", newValue.scale);
        }
    }
}