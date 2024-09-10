using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    Slider gauge;

    protected virtual void Awake()
    {
        Transform child = transform.GetChild(1);
        gauge = child.GetComponent<Slider>();
    }

    /// <summary>
    /// 게이지 설정 함수
    /// </summary>
    /// <param name="ratio">비율</param>
    protected void SetGauge(float ratio)
    {
        gauge.value = ratio;
    }
}