using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject_Product : MoveObject
{
    protected override void OnReachedEndPosition()
    {
        this.gameObject.SetActive(false);
    }
}