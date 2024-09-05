using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 팩토리에서 생산할 오브젝트가 가지고 있는 인터페이스
/// </summary>
public interface IProduct
{
    /// <summary>
    /// 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public Action OnDeactive { get; set; }
}