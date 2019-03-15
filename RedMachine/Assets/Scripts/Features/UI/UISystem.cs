using Assets.Scripts.Features.Times;
using Assets.Scripts.Features.Unit;
using UnityEngine;

namespace Assets.Scripts.Features.UI
{
    public class UISystem : IAttachContext, IStartSystem
    {
        #region Services

        private TimeService
            _time;

        #endregion

        private Context
            _context;

        private Entity
            _timeScaleEntity;

        public void Attach(Context context)
        {
            _context = context;

            _time = _context.services.time;
        }

        public void OnStart()
        {
            _context.services.pool.Provide<UnitComponent>()
                .AddListener(GameObject.Find("Canvas/GameHud").GetView<GameHudView>());

            var timeScaleView = GameObject.Find("Canvas/TimeScale").GetView<TimeScaleView>();

            _timeScaleEntity = _context.entities.NewEntity();
            _timeScaleEntity.Add(new TimeScaleComponent { scale = _time.GetTimeScale() })
                .AddListener(timeScaleView);

            timeScaleView.OnSliderValueChanged += (timeScale) =>
            {
                _time.SetTimeScale(timeScale);

                _timeScaleEntity.Get<TimeScaleComponent>().Set(value => value.scale = timeScale);
            };
        }
    }
}
