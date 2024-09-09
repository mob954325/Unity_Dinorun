using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour, IProduct
{
    Collider2D coll;

    public Action OnDeactive { get; set; }

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true; // 트리거 초기화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger(collision.gameObject.GetComponent<CharacterBase>());
    }

    private void OnDisable()
    {
        OnDeactive?.Invoke();
    }

    /// <summary>
    /// 트리거 될 때 실행되는 함수
    /// </summary>
    protected virtual void OnTrigger(CharacterBase player)
    {
        // 트리거 내용
    }
}