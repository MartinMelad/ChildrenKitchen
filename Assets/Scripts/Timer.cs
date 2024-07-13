using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public void StartTimer(float saconds)
    {
        StartCoroutine(Wait(saconds));
    }

    IEnumerator Wait(float saconds)
    {
        yield return new WaitForSeconds(saconds);
        EventManager.Instance.OnTimerEnd.Invoke();

    }
}
