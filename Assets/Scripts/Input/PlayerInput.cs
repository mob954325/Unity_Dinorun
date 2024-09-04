using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    /// <summary>
    /// 점프 시 실행하는 델리게이트 (space)
    /// </summary>
    public Action OnJump;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += OnJumpInput;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= OnJumpInput;
        playerInputActions.Player.Disable();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}