using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObject : MonoBehaviour
{
    Rigidbody2D rigid;

    public float moveSpeed = 5f;

    /// <summary>
    /// 다시 되돌아가는 좌표 x값
    /// </summary>
    public float reset_x = 30f;

    /// <summary>
    /// 디버그 할 때 활성화 하는 bool값 ( true면 정지 )
    /// </summary>
    public bool isDebug = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDebug) // 디버그 모드면 무시
            return;

        if(transform.position.x < 0.1f)
        {
            OnReachedEndPosition();
        }

        transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// 정해진 위치까지 도달하면 실행하는 함수
    /// </summary>
    protected virtual void OnReachedEndPosition()
    {
        transform.position = new Vector3(reset_x, transform.position.y, transform.position.z); // 특정 위치에 가면 위치 초기화
    }
}