﻿using Assets.Scripts.Features.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.UI
{
    public class GameHudView : ViewBase, IPoolListener<UnitComponent>
    {
        #region Bindings

        [SerializeField]
        private LayoutElement 
            _redElement;

        [SerializeField]
        private LayoutElement 
            _blueElement;

        #endregion

        public void OnChange(ComponentPool<UnitComponent> pool)
        {
            int blueCount = 0;
            int redCount = 0;

            for (int i = 0; i < pool.Items.Count; i++)
            {
                var item = pool.Items[i];

                switch (item.value.type)
                {
                    case UnitType.Blue:
                        blueCount++;
                        break;
                    case UnitType.Red:
                        redCount++;
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException(item.value.type.ToString());
                }
            }

            _redElement.gameObject.SetActive(redCount > 0);
            _blueElement.gameObject.SetActive(blueCount > 0);

            if (redCount > 0
                && blueCount > 0)
            {
                _redElement.flexibleWidth = redCount;
                _blueElement.flexibleWidth = blueCount;
            }
        }
    }
}