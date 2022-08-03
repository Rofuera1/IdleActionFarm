using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public Text GrassCapacity;

        public void UpdateGrassCapacity(int newCapacityCurrent, int maxCapacity)
        {
            GrassCapacity.text = newCapacityCurrent.ToString() + "/" + maxCapacity.ToString();
        }
    }
}
