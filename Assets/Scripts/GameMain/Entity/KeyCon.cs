using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCon : GameBehavior
{
   public bool isCanKeyCon = true;
    protected  bool willUseRigidBody;
    protected Rigidbody2D rd;
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
    public virtual void beCon()
    {

    }
    public bool keyMouse0Down()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool keyMouse0Up()
    {
        return Input.GetMouseButtonUp(0);
    }
    public bool keyMouse0On()
    {
        return Input.GetMouseButton(0);
    }
    public bool keyMouse1Down()
    {
        return Input.GetMouseButtonDown(1);
    }
    public bool keyMouse1Up()
    {
        return Input.GetMouseButtonUp(1);
    }
    public bool keyMouse1On()
    {
        return Input.GetMouseButton(1);
    }

    public bool keyDown(KeyCode k)
    {
        return Input.GetKeyDown(k);
    }
    public bool keyUp(KeyCode k)
    {
        return Input.GetKeyUp(k);
    }
    public bool keyOn(KeyCode k)
    {
        return Input.GetKey(k);
    }
    public bool keyOff(KeyCode k)
    {
        return !Input.GetKey(k);
    }
}
