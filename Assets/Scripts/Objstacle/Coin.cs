using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Obstacle
{
    /// <summary>
    /// 얻을 스코어량
    /// </summary>
    public int scoreValue = 0;

    protected override void OnTrigger(CharacterBase player)
    {
        if (player != null)
        {
            player.GetScore(scoreValue);
            transform.parent.gameObject.SetActive(false);   // 오브젝트 비활성화
        }
    }
}