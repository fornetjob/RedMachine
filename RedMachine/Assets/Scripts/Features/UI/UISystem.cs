using Assets.Scripts.Features.Board;
using Assets.Scripts.Features.Events;
using Assets.Scripts.Features.Times;
using Assets.Scripts.Features.Unit;

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

        private GameEndView
            _gameEndView;

        #endregion

        #region IBeginSystem

        void IBeginSystem.OnBegin(Context context)
        {
            _context = context;

            _context.services.pool.Provide<EventListenerComponent>()
                .Create().value = this;

            _context.services.pool.Provide<UnitComponent>()
                .OnCountChanged.AddListener(_context.services.view.Attach<GameHudView>("Canvas/GameHud"));

            _context.services.view.AttachChildren<ButtonView>("Canvas");

            _gameEndView = _context.services.view.Attach<GameEndView>("Canvas/GameEnd");

            var gameBoardPool = _context.services.pool.Provide<BoardStateComponent>();

            gameBoardPool.AddListeners(gameBoardPool.Single().Id, _gameEndView);

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
                case GameHudView.OnGameEnd:

                    var state = _context.services.pool.Provide<BoardStateComponent>().Single();

                    if (state.Type == BoardStateType.Move)
                    {
                        state.Type = BoardStateType.GameEnd;
                    }

                    if (state.Type == BoardStateType.GameEnd)
                    {
                        _gameEndView.Set((UnitType)e.value,
                            _context.services.pool.Provide<GameTimeComponent>().Single().unscaledGameTime);
                    }

                    break;
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