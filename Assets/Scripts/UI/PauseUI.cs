using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(() => GameManager.Instance.soundManager.PlayAudio(0));
        btn.onClick.AddListener(() => GameManager.Instance.Pause());
    }
}