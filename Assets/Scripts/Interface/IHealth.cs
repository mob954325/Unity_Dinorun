using System;
using System.Collections.Generic;

/// <summary>
/// 체력 인터페이스
/// </summary>
public interface IHealth
{
    /// <summary>
    /// 현재 체력
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public int MaxHealth { get; set; }

    /// <summary>
    /// 피격 시 실행되는 함수
    /// </summary>
    public void OnHit();

    /// <summary>
    /// 사망 시 실행되는 함수
    /// </summary>
    public void OnDie();
}