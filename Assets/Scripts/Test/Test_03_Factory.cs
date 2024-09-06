using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03_Factory : TestBase
{
    public FactoryBase factory;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        factory.InstantiateProduct(Vector3.zero, Quaternion.identity);
    }
}
