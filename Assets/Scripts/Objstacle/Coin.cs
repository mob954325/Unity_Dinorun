using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Obstacle, IProduct
{
    /// <summary>
    /// 얻을 스코어량
    /// </summary>
    public int scoreValue = 0;

    public Action OnDeactive { get; set; }

    private void OnDisable()
    {
        OnDeactive?.Invoke();
    }

    protected override void OnTrigger(CharacterBase player)
    {
        if (player != null)
        {
            player.GetScore(scoreValue);
        }
    }
}