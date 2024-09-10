using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : GaugeUI
{
    CharacterBase player;

    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<CharacterBase>();

        player.OnHpRatioChange += SetGauge;
    }
}