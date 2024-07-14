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

        //�ص�������ÿ����һ���µĶ�����ͼ��Ͼͺ���
        inputActions.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    /// <summary>
    /// ����Gameplay������
    /// </summary>
    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();

        //���������,�������
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

    }

    /// <summary>
    /// ������������
    /// </summary>
    public void DisableAllInputs()
    {
        inputActions.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //�������붯������ȡ���Ķ�ά������ֵ
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }

}
