using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    private ScoreUI scoreUI;
    private FeverUI feverUI;
    private HpUI hpUI;

    private void Awake()
    {
        scoreUI = FindAnyObjectByType<ScoreUI>();
        feverUI = FindAnyObjectByType<FeverUI>();
        hpUI = FindAnyObjectByType<HpUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart(out CharacterBase player);

        scoreUI.Init(player);
        feverUI.Init(player);
        hpUI.Init(player);

        player.Init(); // 캐릭터 초기화
    }
}