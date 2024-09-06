using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ChracterType
{
    None = 0,
    normal,     // 일반 타입 : 점수 두배
    immune,     // 무적
    magnet,     // 끌어당기기
    healthgen   // 체력 회복
}

/// <summary>
/// 캐릭터 베이스 스크립트
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public abstract class CharacterBase : MonoBehaviour, IHealth
{
    private Rigidbody2D rigid;
    protected PlayerInput playerInput;

    /// <summary>
    /// 이 캐릭터의 캐릭터 타입 ( 캐릭터 구별용 )
    /// </summary>
    [Tooltip("캐릭터 타입은 인스펙터에서 초기화 해야함")]
    public ChracterType type = ChracterType.None;

    /// <summary>
    /// 캐릭터 스프라이트 색
    /// </summary>
    private SpriteRenderer sprite;

    private float score = 0;

    /// <summary>
    /// 스코어 획득 배율 ( 기본 1.0 )
    /// </summary>
    private float scoreRatio = 1.0f;

    protected float Score
    {
        get => score;
        set
        {
            score = value * scoreRatio;
        }
    }

    /// <summary>
    /// 현재 피버 게이지량
    /// </summary>
    private float curFeverAmount;

    /// <summary>
    /// 현재 피버 게이지량 수정 및 접근용 프로퍼티
    /// </summary>
    protected float CurFeverAmount
    {
        get => curFeverAmount;
        set
        {
            curFeverAmount = value;

            if(curFeverAmount > maxFeverAmount)
            {
                curFeverAmount = 0f; // 초기화
                ActiveAbility();
            }
        }
    }

    /// <summary>
    /// 피버 최대량 ( 최대량에 도달하면 능력 발동 )
    /// </summary>
    private const float maxFeverAmount = 100f;

    /// <summary>
    /// 피버타임인지 확인하는 변수 (피버면 true 아니면 false)
    /// </summary>
    protected bool isFeverTime = false;

    /// <summary>
    /// 피버 타임 지속시간
    /// </summary>
    protected const float feverDurationTime = 5f;

    /// <summary>
    /// 현재 체력
    /// </summary>
    private int health = 0;

    public int Health 
    { 
        get => health; 
        set
        {
            health = value;

            if (health < 1)
            {
                OnDie();
            }
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    private int maxHealth = 5;

    public int MaxHealth { get; set; }

    /// <summary>
    /// 점프 파워 (5f)
    /// </summary>
    private const float jumpPower = 7f;

    /// <summary>
    /// 공격을 받았는지 확인하는 변수 ( 일정시간 지난 후 해제 )
    /// </summary>
    public bool isHit = false;

    // 유니티 함수 ======================================================================================================

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Init();
    }

    protected virtual void Update()
    {
        CurFeverAmount += Time.deltaTime;
        //Debug.Log($"피버 게이지량 : {CurFeverAmount}");
    }

    // 기능 함수 ========================================================================================================

    /// <summary>
    /// 캐릭터 클래스 초기화 함수
    /// </summary>
    protected virtual void Init()
    {
        curFeverAmount = 0f;
        Score = 0;
        MaxHealth = maxHealth;
        Health = maxHealth; // 체력 초기화

        playerInput.OnJump += Jump;
        Debug.Log("캐릭터 초기화 완료");
    }

    /// <summary>
    /// 캐릭터 능력 함수, CurFeverAmount가 maxFeverAmount를 초과하면 실행됨
    /// </summary>
    protected virtual void ActiveAbility()
    {
        // 캐릭터 능력 내용
        Debug.Log("능력 발동");
        StartCoroutine(ActiveFeverTime());
    }

    /// <summary>
    /// 캐릭터 점프 함수
    /// </summary>
    protected virtual void Jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        Debug.Log("플레이어 점프");
    }

    /// <summary>
    /// 피버타임이 활성화되면 실행되는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActiveFeverTime()
    {
        float timeElapsed = 0f;

        isFeverTime = true;
        while (timeElapsed < feverDurationTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isFeverTime = false;
    }

    /// <summary>
    /// 공격 맞았을 때 실행하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitEffect()
    {
        float timeElapsed = 0f;

        isHit = true;
        sprite.color = new Color(1f, 1f, 1f, 0.25f);

        while (timeElapsed < feverDurationTime)
        {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        sprite.color = new Color(1f, 1f, 1f, 1f);
        isHit = false;
    }

    // IHealth ========================================================================================================

    public void OnHit()
    {
        StartCoroutine(HitEffect());
        Health--;
        Debug.Log("플레이어 데미지받음");
    }

    public void OnDie()
    {
        // 사망
        Debug.Log("플레이어 사망");
    }

    // 점수 관련 ========================================================================================================
    
    /// <summary>
    /// 스코어 획득 비율 설정 함수
    /// </summary>
    /// <param name="ratio">조정할 비율량 (기본값 : 1.0f)</param>
    protected void SetScoreRatio(float ratio = 1.0f)
    {
        scoreRatio = ratio;
    }

    /// <summary>
    /// 코인 획득 함수
    /// </summary>
    /// <param name="value">획득량</param>
    public void GetScore(int value)
    {
        Debug.Log($"획득 전 스코어 : {Score}");

        Score += value;

        Debug.Log($"획득 후 스코어 : {Score}");
    }
}