// ----- C#
using InGame.ForAbility;
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForBuff.ForUI
{
    public class UnitBuffView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<UnitBuffItemView> _itemViewList = new List<UnitBuffItemView>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<EAbilityType, UnitBuffItemView> _itemViewSet = new Dictionary<EAbilityType, UnitBuffItemView>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit()
        {
            for (int i = 0; i < _itemViewList.Count; i++)
            {
                var itemView = _itemViewList[i];
                _itemViewSet.Add(itemView.BuffType, itemView);

                itemView.gameObject.SetActive(false);
            }
        }

        public void ShowToBuffItemView(EAbilityType buffType, float buffDuration, Action doneCallBack = null)
        {
            if (_itemViewSet.TryGetValue(buffType, out var buffItemView))
            {
                buffItemView.gameObject.SetActive(true);
                buffItemView.Show(buffDuration, doneCallBack);
            }
            else Debug.LogError($"<color=red>[UnitBuffView.ShowToVBuffItemView] {buffType}�� �ش��ϴ� Buff Item View�� �������� �ʽ��ϴ�.</color>");
        }
    }
}