using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public enum ChracterType
{
    None = 0,
    normal,     // 일반 타입 : 점수 두배
    healthgen   // 체력 회복 : 장애물에 부딪히면 체력 회복
}

/// <summary>
/// 캐릭터 베이스 스크립트
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public abstract class CharacterBase : MonoBehaviour, IHealth
{
    GameManager manager;

    private Rigidbody2D rigid;
    private Animator anim;
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

    /// <summary>
    /// 캐릭터 스코어
    /// </summary>
    private float score = -1;

    /// <summary>
    /// 스코어 획득 배율 ( 기본 1.0 )
    /// </summary>
    private float scoreRatio = 1.0f;

    protected float Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreChange?.Invoke(score);
        }
    }

    /// <summary>
    /// 현재 피버 게이지량
    /// </summary>
    private float curFeverAmount = -1;

    /// <summary>
    /// 현재 피버 게이지량 수정 및 접근용 프로퍼티
    /// </summary>
    protected float CurFeverAmount
    {
        get => curFeverAmount;
        set
        {
            curFeverAmount = value;
            OnFeverRatioChange?.Invoke(curFeverAmount/maxFeverAmount);

            if (curFeverAmount > maxFeverAmount)
            {
                curFeverAmount = 0f; // 초기화
                ActiveAbility();
            }
        }
    }

    /// <summary>
    /// 피버 최대량 ( 최대량에 도달하면 능력 발동 )
    /// </summary>
    private const float maxFeverAmount = 10f;

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
    [SerializeField]
    private float health = -1;

    public float Health 
    { 
        get => health; 
        set
        {
            health = value;
            OnHpRatioChange?.Invoke(health/maxHealth);

            if (health < 1)
            {
                OnDie();
            }
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    private float maxHealth = 100;

    public float MaxHealth { get; set; }

    /// <summary>
    /// 체력 감소 비율
    /// </summary>
    private float healthReduceRatio = 2.5f;

    /// <summary>
    /// 점프 파워 (5f)
    /// </summary>
    private const float jumpPower = 7f;

    /// <summary>
    /// 땅에 있는지 확인하는 변수 (땅에 있으면 true 아니면 false)
    /// </summary>
    private bool isGround = true;

    /// <summary>
    /// 공격을 받았는지 확인하는 변수 ( 일정시간 지난 후 해제 )
    /// </summary>
    public bool isHit = false;

    /// <summary>
    /// 스코어가 변했을 때 호출되는 델리게이트
    /// </summary>
    public Action<float> OnScoreChange;

    /// <summary>
    /// 피버값이 변했을 때 호출되는 델리게이트
    /// </summary>
    public Action<float> OnFeverRatioChange;

    /// <summary>
    /// 체력값이 변했을 때 호출되는 델리게이트
    /// </summary>
    public Action<float> OnHpRatioChange;

    /// <summary>
    /// isPause 애니메이션 파라미터 (bool)
    /// </summary>
    int isPauseToHash = Animator.StringToHash("isPause");

    /// <summary>
    /// isJump 애니메이션 파라미터 (bool)
    /// </summary>
    int isJumpToHash = Animator.StringToHash("isJump");

    /// <summary>
    /// die 애니메이션 파라미터 (trigger)
    /// </summary>
    int dieToHash = Animator.StringToHash("die");

    // 유니티 함수 ======================================================================================================

    protected virtual void Awake()
    {
        Transform child = transform.GetChild(0);

        manager = GameManager.Instance;
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sprite = child.GetComponent<SpriteRenderer>();
        anim = child.GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (!manager.isPlaying)
            return;

        if(!isFeverTime)
        {
            CurFeverAmount += Time.deltaTime;
        }

        if(health > 0f)
        {
            health -= Time.deltaTime * healthReduceRatio;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            anim.SetBool(isJumpToHash, !isGround);
        }
    }

    // 기능 함수 ========================================================================================================

    /// <summary>
    /// 캐릭터 클래스 초기화 함수
    /// </summary>
    public virtual void Init()
    {
        curFeverAmount = 0f;
        Score = 0;
        MaxHealth = maxHealth;
        Health = maxHealth; // 체력 초기화

        playerInput.OnJump += Jump;
        manager.OnGamePlay += Animation_RunPlay;
        manager.OnGamePause += Animation_RunStop;
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
        if(!isGround) return; 

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGround = false;

        anim.SetBool(isJumpToHash, !isGround);
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
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.25f);

        while (timeElapsed < feverDurationTime)
        {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        isHit = false;
    }

    public bool CheckIsFever()
    {
        return isFeverTime;
    }

    private void Animation_RunStop()
    {
        anim.SetBool(isPauseToHash, true);
    }

    private void Animation_RunPlay()
    {
        anim.SetBool(isPauseToHash, false);
    }

    // IHealth ========================================================================================================

    protected void Addhealth(float value)
    {
        health += value;
    }

    public void OnHit()
    {
        StartCoroutine(HitEffect());
        Health--;
    }

    public void OnDie()
    {
        // 사망
        Debug.Log("플레이어 사망");
        anim.SetTrigger(dieToHash);
        manager.OnGameOver?.Invoke(score);
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
        Score += value * scoreRatio;
    }
}