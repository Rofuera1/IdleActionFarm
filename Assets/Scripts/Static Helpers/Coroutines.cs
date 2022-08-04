using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ref<T>
{
    private T _value;
    public T Value { get { return _value; } set { _value = value; } }

    public Ref(T reference)
    {
        _value = reference;
    }
}

public static class Coroutines
{
    public delegate float whatFuncToUse(float a, float b, float time);

    /*public static IEnumerator spawnTextLerpTo(GameObject prefabText, Vector3 atPos, Transform lerpTo, float waitTimeBeforeLerp, float lerpTime, float timeBeforeSqueeze)
    {
        GameObject obj = Instantiate(prefabText, can.transform);
        obj.transform.position = atPos;

        yield return new WaitForSeconds(waitTimeBeforeLerp);

        StartCoroutine(lerpObject(obj, lerpTo, lerpTime, EasingFunction.EaseInOutCirc));
        StartCoroutine(squeezeObject(obj, Vector3.zero, lerpTime - timeBeforeSqueeze, EasingFunction.EaseInOutCirc, true, timeBeforeSqueeze));
    }

    public static IEnumerator spawnReward(int howMuch, float range, float rangeTime, float waitTime, float lerpTimeMin, float lerpTimeMax, float lerpDelay, Vector3 spawnWhere, Transform moveWhere, int addMoney)
    {
        GameObject[] rewards = new GameObject[howMuch];
        Debug.Log("Spawning " + howMuch);

        StartCoroutine(lerpObject(moneyParent, moneyParentStartPos - Vector3.right * moneyParent.GetComponent<RectTransform>().rect.width / 2, 0.3f, EasingFunction.EaseInOutCirc));

        for (int i = 0; i < howMuch; i++)
        {
            Vector3 direction = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f).normalized;

            rewards[i] = Instantiate(rewardPrefab, can.transform);
            rewards[i].transform.position = spawnWhere;
            StartCoroutine(lerpObject(rewards[i], spawnWhere + direction * range, rangeTime, EasingFunction.EaseInOutCirc));
        }

        yield return new WaitForSeconds(rangeTime + waitTime);

        StartCoroutine(lerpTextInt(moneyText, money - addMoney, money, 0.5f, EasingFunction.EaseInOutCirc, (lerpTimeMin + lerpTimeMax) / 2));

        for (int i = 0; i < howMuch; i++)
        {
            StartCoroutine(lerpObject(rewards[i], moveWhere, Random.Range(lerpTimeMin, lerpTimeMax), EasingFunction.EaseInOutCirc));
            Destroy(rewards[i], lerpTimeMax);
            yield return new WaitForSeconds(lerpDelay);
        }
    }*/

    public static IEnumerator LerpLocalPosition(Transform obj, Vector3 endLocalPosition, float time, whatFuncToUse func)
    {
        Vector3 startLocalPosition = endLocalPosition;
        float t = 0f;

        while(t < time)
        {
            t += Time.deltaTime;

            obj.transform.localPosition = Vector3.Lerp(startLocalPosition, endLocalPosition, func(0f, 1f, t / time));

            yield return null;
        }

        obj.transform.localPosition = endLocalPosition;
    }

    public static IEnumerator SetActiveAfterTime(GameObject obj, float time, bool enabled)
    {
        yield return new WaitForSeconds(time);

        obj.SetActive(enabled);
    }

    public static IEnumerator LerpScale(Transform obj, Vector3 endScale, float time, whatFuncToUse func)
    {
        Vector3 startScale = obj.transform.localScale;
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(startScale, endScale, func(0f, 1f, t / time));

            yield return null;
        }

        obj.transform.localScale = endScale;
    }

    public static IEnumerator LerpTextIntWithParse(Text text, int to, float time, whatFuncToUse easingFunc, float waitTime = 0f)
    {
        float t = 0f;
        int from = int.Parse(text.text);
        yield return new WaitForSeconds(waitTime);

        while (t < time)
        {
            t += Time.deltaTime;

            text.text = ((int)Mathf.Lerp(from, to, easingFunc(0f, 1f, t / time))).ToString();

            yield return null;
        }

        text.text = to.ToString();
    }

    public static IEnumerator LerpTextInt(Text text, int from, int to, float time, whatFuncToUse easingFunc, float waitTime = 0f)
    {
        float t = 0f;
        yield return new WaitForSeconds(waitTime);

        while (t < time)
        {
            t += Time.deltaTime;

            text.text = ((int)Mathf.Lerp(from, to, easingFunc(0f, 1f, t / time))).ToString();

            yield return null;
        }

        text.text = to.ToString();
    }

    public static IEnumerator lerpObject(GameObject obj, Vector3 pos, float time, whatFuncToUse easingFunc)
    {
        float t = 0f;
        Vector3 startPos = obj.transform.position;

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.position = Vector3.Lerp(startPos, pos, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }

    public static IEnumerator lerpObject(GameObject obj, Transform pos, float time, whatFuncToUse easingFunc)
    {
        float t = 0f;
        Vector3 startPos = obj.transform.position;

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.position = Vector3.Lerp(startPos, pos.position, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }

    public static IEnumerator squeezeObject(GameObject obj, Vector3 squeezeTill, float time, whatFuncToUse easingFunc, float waitTime = 0f)
    {
        float t = 0f;
        Vector3 startScale = obj.transform.localScale;

        yield return new WaitForSeconds(time);

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(startScale, squeezeTill, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }

    public static IEnumerator linearLerpImageFll(Image slider, float till, float time)
    {
        float t = 0f;
        float a = slider.fillAmount;

        while (t < time)
        {
            t += Time.deltaTime;

            slider.fillAmount = Mathf.Lerp(a, till, t / time);

            yield return null;
        }
    }

    public static IEnumerator printText(Text whereToPrint, string whatToPrint, float time, Ref<bool> boolTextPrinting)
    {
        float printEvery = time / whatToPrint.Length;
        whereToPrint.text = "";
        boolTextPrinting.Value = true;

        bool gettingNewRichStart = false, gettingNewRichEnd = false;
        string riches = "";
        int currentId = -1;
        int inputIndex = 0;

        for (int i = 0; i < whatToPrint.Length; i++)
        {
            if (whatToPrint[i] == '<')
            {
                if (whatToPrint[i + 1] == '/')
                {
                    gettingNewRichEnd = true;
                }
                else
                {
                    gettingNewRichStart = true;
                    riches = "<";
                    currentId++;
                }
            }
            else if (whatToPrint[i] == '>')
            {
                if (gettingNewRichStart)
                {
                    gettingNewRichStart = false;
                    riches += ">";

                    if (currentId == 0)
                    {
                        whereToPrint.text += riches + reversedRich(riches);
                    }
                    else
                    {
                        whereToPrint.text = whereToPrint.text.Insert(inputIndex, riches + reversedRich(riches));
                        inputIndex += riches.Length;
                    }

                    inputIndex = i + 1;
                }
                else if (gettingNewRichEnd)
                {
                    gettingNewRichEnd = false;

                    riches = "";
                    currentId--;
                }
            }
            else if (gettingNewRichStart || gettingNewRichEnd)
            {
                riches += whatToPrint[i];
            }
            else
            {
                yield return new WaitForSeconds(printEvery);

                if (currentId != -1)
                {
                    Debug.Log(inputIndex);
                    whereToPrint.text = whereToPrint.text.Insert(inputIndex, whatToPrint[i].ToString());
                    inputIndex++;
                }
                else
                    whereToPrint.text += whatToPrint[i];
            }

            if (!boolTextPrinting.Value)
                break;
        }


        boolTextPrinting.Value = false;
        whereToPrint.text = whatToPrint;
    }

    public static string reversedRich(string normalRich)
    {
        string result = "</" + normalRich.Substring(1, normalRich.Length - 1);

        if (normalRich.Contains("color"))
            result = "</color>";

        return result;
    }

    public static IEnumerator uiBounce(GameObject obj, float time, whatFuncToUse easingFunc)
    {
        float t = 0f;
        float mult = 1.05f;
        obj.SetActive(true);

        while (t < time / 2)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * mult, easingFunc(0f, 1f, t / time));

            yield return null;
        }

        t = 0f;

        while (t < time / 2)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(Vector3.one * mult, Vector3.one, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }

    public static IEnumerator uiAppear(GameObject obj, float time, whatFuncToUse easingFunc)
    {
        float t = 0f;
        obj.SetActive(true);

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }

    public static IEnumerator uiDisappear(GameObject obj, float time, whatFuncToUse easingFunc)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;

            obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, easingFunc(0f, 1f, t / time));

            yield return null;
        }
    }
}
