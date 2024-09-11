using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObject : MonoBehaviour
{
    GameManager manager;

    Rigidbody2D rigid;

    public float moveSpeed = 5f;

    /// <summary>
    /// 다시 되돌아가는 좌표 x값
    /// </summary>
    public float reset_x = 30f;

    /// <summary>
    /// 멈출지 확인하는 변수 (true면 정지)
    /// </summary>
    public bool isStop = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        manager = GameManager.Instance;

        manager.OnGamePlay += SetUnStop;
        manager.OnGamePause += SetStop;        
    }

    private void FixedUpdate()
    {
        if (isStop)
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

    private void SetStop()
    {
        isStop = true;
    }

    private void SetUnStop()
    {
        isStop = false;
    }
}