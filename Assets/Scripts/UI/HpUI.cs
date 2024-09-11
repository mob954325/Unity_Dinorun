using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : GaugeUI
{
    public override void Init(CharacterBase player)
    {
        player.OnHpRatioChange += SetGauge;
    }
}