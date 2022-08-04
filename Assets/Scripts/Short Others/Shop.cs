using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class Shop : MonoBehaviour
    {
        private int goldForSingleGrassCost;
        private int currentGoldAmount;

        public void Init(StatsProperty stats)
        {
            currentGoldAmount = 0;
            goldForSingleGrassCost = stats.GoldForSingleGrass;
        }

        public void ReceiveGrassForSelling(GameObject grassObject)
        {
            changeGoldAmount(currentGoldAmount + goldForSingleGrassCost);
            moveGrassToShop(grassObject.transform);
        }

        private void moveGrassToShop(Transform grassTransform)
        {
            float LERP_TIME = 1f;
            grassTransform.DOMove(transform.position, LERP_TIME);
            grassTransform.DOScale(Vector3.zero, LERP_TIME);
            Destroy(grassTransform.gameObject, LERP_TIME * 2f);
        }

        private void changeGoldAmount(int newAmount)
        {
            currentGoldAmount = newAmount;
            UIManager.AddedMoney(currentGoldAmount);
        }
    }
}
