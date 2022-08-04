using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        protected static UIManager Instance;

        public Text GrassCapacity;
        public Text MoneyAmount;

        public void Init(StatsProperty stats)
        {
            Instance = this;
            UpdateGrassCapacity(0, stats.PlayerMaxGrassCapacity);
            AddedMoney(0);
        }

        public static void UpdateGrassCapacity(int newCapacityCurrent, int maxCapacity)
        {
            Instance.GrassCapacity.text = newCapacityCurrent.ToString() + "/" + maxCapacity.ToString();
        }

        public static void AddedMoney(int moneyAddedAmount)
        {
            Instance.MoneyAmount.text = moneyAddedAmount.ToString();
        }
    }
}
