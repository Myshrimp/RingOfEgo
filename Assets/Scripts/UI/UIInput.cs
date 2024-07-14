using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

///<summary>
///
///<summary>

public class UIInput : Singleton<UIInput>
{
    // [SerializeField] PlayerInput playerInput;
    InputSystemUIInputModule UIInputModule;
    protected override void Awake()
    {
        base.Awake();
        UIInputModule = GetComponent<InputSystemUIInputModule>();
        UIInputModule.enabled = false;
    }

    public void SelectUI(Selectable UIObject)
    {
        // UIObject.Select();
        // UIObject.OnSelect(null);
        UIInputModule.enabled = true;
    }
    //Ω˚÷πÀ˘”– ‰»Î
    public void DisableAllUIInputs()
    {
        // playerInput.DisableAllInputs();
        UIInputModule.enabled = false;
    }
}
