using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAddition : GameBehavior
{
   public bool isCanKeyCon = true;
    public bool isCanUseKeyNow = true;
    protected  bool willUseRigidBody;
    [HideInInspector]
    public Rigidbody2D rd;
    public override void awake_()
    {
        base.awake_();
        if (willUseRigidBody)
        {
            rd = GetComponentInParent<Rigidbody2D>();
            if (rd == null) rd = GetComponentInChildren<Rigidbody2D>();
            if (rd == null) Debug.LogWarning("你完全无刚体");
            
        }

    }
    public virtual void beActive()
    {
        if (isCanKeyCon)
        {
            isCanUseKeyNow = getCanUseKey();
        }
    }
    public bool keyMouse0Down()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButtonDown(0);
    }
    public bool keyMouse0Up()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButtonUp(0);
    }
    public bool keyMouse0On()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButton(0);
    }
    public bool keyMouse1Down()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButtonDown(1);
    }
    public bool keyMouse1Up()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButtonUp(1);
    }
    public bool keyMouse1On()
    {
        if (!isCanUseKey()) return false;
        return Input.GetMouseButton(1);
    }

    public bool keyDown(KeyCode k)
    {
        if (!isCanUseKey()) return false;
        return Input.GetKeyDown(k);
    }
    public bool keyUp(KeyCode k)
    {
        if (!isCanUseKey()) return false;
        return Input.GetKeyUp(k);
    }
    public bool keyOn(KeyCode k)
    {
        if (!isCanUseKey()) return false;
        return Input.GetKey(k);
    }
    public bool keyOff(KeyCode k)
    {

        if (!isCanUseKey()) return false;
        return !Input.GetKey(k);
    }
    public bool isCanUseKey()
    {
        return isCanUseKeyNow;
    }
    public bool getCanUseKey()
    {
        if (entity != null && !entity.isKeyCon) return false;
        if (!isKeyCon) return false;
        return true;
    }
}
