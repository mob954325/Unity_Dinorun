using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void SetGameOverScore(float score)
    {
        this.gameObject.SetActive(true);
        scoreText.text = $"{score}";
    }
}