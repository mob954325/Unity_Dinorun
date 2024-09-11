using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtnUI : MonoBehaviour
{
    private Button startBtn;
    private Button exitBtn;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        startBtn = child.GetComponent<Button>();

        child = transform.GetChild(1);
        exitBtn = child.GetComponent<Button>();
    }

    /// <summary>
    /// 게임 퇴장시 실행하는 함수
    /// </summary>
    private void OnExitGame()
    {
        Application.Quit();
    }
}
