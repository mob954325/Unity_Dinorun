using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SoundManager soundManager;

    public GameObject[] characters;

    /// <summary>
    /// 정지 UI
    /// </summary>
    private Transform pausePanel;

    /// <summary>
    /// 게임오버 UI
    /// </summary>
    private GameOverUI gameOverUI;

    /// <summary>
    /// 선택된 캐릭터 타입
    /// </summary>
    private int selectedType = -1;

    /// <summary>
    /// 게임 중인지 확인하는 변수
    /// </summary>
    public bool isPlaying = false;

    /// <summary>
    /// 게임이 일시중지되었을 때 델리게이트
    /// </summary>
    public Action OnGamePause;
    
    /// <summary>
    /// 게임이 시작될 때 호출되는 델리게이트
    /// </summary>
    public Action OnGamePlay;

    /// <summary>
    /// 게임 오버되었을 때 호출되는 델리게이트
    /// </summary>
    public Action<float> OnGameOver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Transform child = transform.GetChild(0);

        pausePanel = child.GetComponent<Transform>();
        pausePanel.gameObject.SetActive(false);

        child = transform.GetChild(1);
        gameOverUI = child.GetComponent<GameOverUI>();
        OnGameOver += (score) => 
        {
            isPlaying = false;
            gameOverUI.SetGameOverScore(score);
            OnGamePause?.Invoke();
        };

        child = transform.GetChild(2);
        soundManager = child.GetComponent<SoundManager>();

        gameOverUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 메인 메뉴에서 캐릭터 선택시 호출되는 함수
    /// </summary>
    /// <param name="typeInt">캐릭터 타입 int형</param>
    public void OnSelectCharacter(int typeInt)
    {
        selectedType = typeInt;     // 선택한 캐릭터 값 저장
        SceneManager.LoadScene(1);  // 씬 전환
    }

    public void OnGameStart(out CharacterBase characterBase)
    {
        characterBase = Instantiate(characters[selectedType]).GetComponent<CharacterBase>();
        isPlaying = true;
    }

    /// <summary>
    /// 게임 도중 멈출 때 호출되는 함수
    /// </summary>
    public void Pause()
    {
        isPlaying = false;
        pausePanel.gameObject.SetActive(true);
        OnGamePause?.Invoke();
    }

    public void UnPause()
    {
        isPlaying = true;
        pausePanel.gameObject.SetActive(false);
        OnGamePlay?.Invoke();
    }

    public void BeckToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
