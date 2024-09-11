using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void Init(CharacterBase player)
    {
        player.OnScoreChange += SetScoreText;        
    }

    /// <summary>
    /// 스코어 텍스트 설정 함수
    /// </summary>
    /// <param name="score">스코어 값</param>
    public void SetScoreText(float score)
    {
        scoreText.text = $"{score:F0}";
    }    
}
