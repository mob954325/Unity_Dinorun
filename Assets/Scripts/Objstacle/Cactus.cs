using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Obstacle
{
    protected override void OnTrigger(CharacterBase player)
    {
        IHealth health = player as IHealth;

        if (health != null)
        {
            health.OnHit();
        }
    }
}