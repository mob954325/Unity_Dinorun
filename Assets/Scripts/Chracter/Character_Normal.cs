using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Normal : CharacterBase
{
    protected override void ActiveAbility()
    {
        base.ActiveAbility();
        StartCoroutine(SetScore(2.0f));
    }

    IEnumerator SetScore(float value)
    {
        float timeElapsed = 0;

        SetScoreRatio(value);
        while (timeElapsed < feverDurationTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SetScoreRatio();
    }
}