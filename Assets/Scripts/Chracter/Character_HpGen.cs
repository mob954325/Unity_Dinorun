using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_HpGen : CharacterBase
{
    /// <summary>
    /// 선인장 쳤을 때 실행하는 함수
    /// </summary>
    public void OnCactusHit()
    {
        GetScore(100);
        Addhealth(2f);
    }
}
