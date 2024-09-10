using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 정지 UI
    /// </summary>
    private Transform pausePanel;

    /// <summary>
    /// 게임 중인지 확인하는 변수
    /// </summary>
    public bool isPlaying = false;

    public Action OnGameStop;
    public Action OnGamePlay;

    private void Awake()
    {
        Transform child = transform.GetChild(0);

        pausePanel = child.GetComponent<Transform>();
        pausePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 게임 시작될 때 호출되는 함수
    /// </summary>
    public void GameStart()
    {
        pausePanel.gameObject.SetActive(false);
        isPlaying = true;
        OnGamePlay?.Invoke();
    }

    /// <summary>
    /// 게임 도중 멈출 때 호출되는 함수
    /// </summary>
    public void Pause()
    {
        isPlaying = false;
        pausePanel.gameObject.SetActive(true);
        OnGameStop?.Invoke();
    }
}
