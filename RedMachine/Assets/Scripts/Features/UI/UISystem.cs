using Assets.Scripts.Features.Events;
using Assets.Scripts.Features.Times;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Features.UI
{
    public class UISystem : SystemBase, IBeginSystem, IEventListener
    {
        #region Fields

        private Context
            _context;

        private Entity
            _timeScaleEntity;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _context = context;

            _context.services.pool.Provide<EventListenerComponent>()
                .Create().value = this;

            //_context.services.pool.Provide<UnitComponent>()
            //    .AddListener(_context.services.view.Attach<GameHudView>("Canvas/GameHud"));

            _context.services.view.Attach<ButtonView>("Canvas/NewButton");
            _context.services.view.Attach<ButtonView>("Canvas/LoadButton");
            _context.services.view.Attach<ButtonView>("Canvas/SaveButton");

            _timeScaleEntity = _context.entities.Create()
                .AddListener(_context.services.view.Attach<TimeScaleView>("Canvas/TimeScale"));

            _timeScaleEntity.Add<TimeScaleComponent>()
                .Scale = _context.services.time.TimeScale;
        }

        #endregion

        #region IEventListener

        void IEventListener.OnEvent(EventComponent e)
        {
            switch (e.name)
            {
                case TimeScaleView.TimeScaleChanged:
                    var timeScale = (float)e.value;

                    _context.services.time.TimeScale = timeScale;
                    _timeScaleEntity.Get<TimeScaleComponent>().Scale = timeScale;
                    break;
                case ButtonView.ButtonClick:
                    var type = (ButtonActionType)e.value;

                    switch (type)
                    {
                        case ButtonActionType.New:
                            SceneManager.LoadScene("Play");
                            break;
                        case ButtonActionType.Load:
                            _context.services.serialize.LoadGame();
                            break;
                        case ButtonActionType.Save:
                            _context.services.serialize.SaveGame(); 
                            break;
                    }

                    break;
            }
        }

        #endregion
    }
}