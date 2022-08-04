using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        protected static UIManager Instance;
        private Camera MainCamera;

        public Text GrassCapacity;
        public Text MoneyAmount;
        [Space]
        public GameObject MoneyEffectPrefab;
        public Transform MoneyAmountImageTR;
        public Transform CanvasTR;

        public void Init(StatsProperty stats, Camera mainCamera)
        {
            Instance = this;

            MainCamera = mainCamera;

            GrassCapacity.text = "0/" + stats.PlayerMaxGrassCapacity.ToString();
            MoneyAmount.text = "0";
        }

        public static void UpdateGrassCapacity(int newCapacityCurrent, int maxCapacity)
        {
            Instance.GrassCapacity.text = newCapacityCurrent.ToString() + "/" + maxCapacity.ToString();
        }

        public static void AddedMoney(int newMoneyAmount, Vector3 globalPositionWhereItHappened)
        {
            Vector2 screenPosition = Instance.MainCamera.WorldToScreenPoint(globalPositionWhereItHappened);
            Instance.CreateMoneyAddingEffect(newMoneyAmount, screenPosition);
        }

        private IEnumerator lerpingTextCoroutine;

        private void CreateMoneyAddingEffect(int newMoneyAmount, Vector2 localPosition)
        {
            float TIME_TO_LERP = 1f;

            GameObject newMoneyEffectObject = Instantiate(MoneyEffectPrefab, CanvasTR);

            newMoneyEffectObject.transform.position = localPosition;
            newMoneyEffectObject.transform.DOMove(MoneyAmountImageTR.position, TIME_TO_LERP)
                .SetEase(Ease.InOutCirc)
                .OnComplete(() => {
                if (lerpingTextCoroutine != null) StopCoroutine(lerpingTextCoroutine);
                lerpingTextCoroutine = Coroutines.LerpTextIntWithParse(MoneyAmount, newMoneyAmount, 0.1f, EasingFunction.Linear);
                StartCoroutine(lerpingTextCoroutine);
            });

            Destroy(newMoneyEffectObject, TIME_TO_LERP * 2);
        }
    }
}
