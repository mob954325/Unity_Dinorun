using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverUI : GaugeUI
{
    public override void Init(CharacterBase player)
    {
        player.OnFeverRatioChange += SetGauge;        
    }
}