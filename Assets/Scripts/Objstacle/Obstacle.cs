using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true; // 트리거 초기화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger(collision.gameObject.GetComponent<CharacterBase>());
    }

    /// <summary>
    /// 트리거 될 때 실행되는 함수
    /// </summary>
    protected virtual void OnTrigger(CharacterBase player)
    {
        // 트리거 내용
    }
}