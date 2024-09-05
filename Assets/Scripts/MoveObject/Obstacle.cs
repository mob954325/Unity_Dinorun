using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealth targetHealth = collision.gameObject.GetComponent<CharacterBase>();

        if(targetHealth != null)
        {
            targetHealth.OnHit();
        }
    }
}