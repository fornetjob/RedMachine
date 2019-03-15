﻿using Assets.Scripts.Features.Bounces;
using Assets.Scripts.Features.Configs;
using Assets.Scripts.Features.Move;
using Assets.Scripts.Features.Unit;
using UnityEngine;

namespace Assets.Scripts.Features.Board
{
    public class BoardSystem : IAttachContext, IStartSystem, IComponentListener<BoardActionComponent>
    {
        #region Factories

        private BoardFactory
            _boardFactory;

        #endregion

        #region Fields

        private Context
            _context;

        private GameConfig
            _config;

        #endregion

        public void Attach(Context context)
        {
            _context = context;

            _boardFactory = new BoardFactory(context);
        }

        public void OnChanged(BoardActionComponent newValue)
        {
            switch (newValue.type)
            {
                case BoardActionType.Add:
                    _context.AddSystem(new UnitAddSystem());
                    break;
                case BoardActionType.Move:
                    _context.AddSystem(new MoveSystem());
                    _context.AddSystem(new BounceSystem());
                    break;
            }
        }

        public void OnStart()
        {
            Physics.autoSimulation = false;
            Physics2D.autoSimulation = false;

            _config = _context.services.config.GetGameConfig();

            var boardEntity = _boardFactory.Create(_config);

            boardEntity.Get<BoardActionComponent>()
                .AddListener(this);
        }
    }
}
