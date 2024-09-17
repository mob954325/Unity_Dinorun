#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03_Factory : TestBase
{
    public FactoryBase factory;
    public Transform spawnPoint;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        factory.InstantiateProduct(spawnPoint.position, Quaternion.identity);
    }
}
#endif