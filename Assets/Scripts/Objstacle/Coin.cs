using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Obstacle
{
    public int scoreValue = 0;

    protected override void OnTrigger(CharacterBase player)
    {
        if (player != null)
        {
            player.GetScore(scoreValue);
        }
    }
}