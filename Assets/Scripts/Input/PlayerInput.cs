using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGameplayActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };

    InputActions inputActions;

    private void OnEnable()
    {
        inputActions = new InputActions();

        //回调函数，每次有一个新的动作表就加上就好了
        inputActions.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    /// <summary>
    /// 启用Gameplay动作表
    /// </summary>
    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();

        //隐藏鼠标光标,锁定光标
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

    }

    /// <summary>
    /// 禁用所有输入
    /// </summary>
    public void DisableAllInputs()
    {
        inputActions.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //传入输入动作所读取到的二维向量的值
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }

}
