using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class equip : MonoBehaviour
{
    public bool isUsingEquip=true;
    private Animator ac;
    public GameObject usingEqup;
    public bool isHardRequireMouse1;
  

    private firer fire;
    public float maxCanFireDegree = 60;
    public enum equipType
    {
        ju=0, gong=1
    }
    public equipType myEquipType;
    // Start is called before the first frame update
    public  void Start()
    {
        start_();
    }
    public virtual void start_()
    {
        ac = GetComponent<Animator>();
        fire = GetComponent<firer>();
    }
    // Update is called once per frame
    public void Update()
    {
        update_();
    }
    public  virtual  void update_()
    {
        module_attIn();
    }
    public void module_attIn()
    {
        //adjustActionType
        ac.SetFloat("attTypeX", myEquipType.GetHashCode());
        ac.SetBool("isConByMouse1", isHardRequireMouse1);
       
        if (Input.GetMouseButtonDown(1))
        {
           // selfAdjust_mouse1Down();
        }
        if (Input.GetMouseButton(1))
        {
           // selfAdjust_mouse1();
        } else
        {
           // selfAdjust_mouse1Not();
        }
       // if(isHardReqiureUpon) ac.SetFloat("attTime", ac.GetFloat("attTime") - Time.deltaTime * 1f);//µÝ¼õµÄ¹¥»÷Ê±¼ä
        
        {
          //  isUsingEquip= ac.GetBool("isEquiping");
        }
        if (Input.GetMouseButtonDown(0))
        {
            selfAdjust_mouse0Down();
        }
    }
    protected virtual void selfAdjust_mouse1Down()
    {
       
        if (isHardRequireMouse1) {
            if (ac.GetBool("isEquiping"))
            {
                ac.ResetTrigger("attStart");
                ac.SetBool("isEquiping",false );
               // isUsingEquip = false;
            }
            else
            {
                ac.SetTrigger("attStart");
               // isUsingEquip = true;
            }
            {
               
            }
        
        } 
        else
        {
            
        }
    }
    protected virtual void selfAdjust_mouse0Down()
    {
        if (isUsingEquip)
            if (myEquipType == equipType.gong)
            {
                float fP = GetComponent<conPlayer>().faceTo;
               // float f2 = Mathf.Atan2(fire.faceTo.y, fire.faceTo.x) * Mathf.Rad2Deg;
                //  Debug.Log("fp"+fP);
             //   Debug.Log(fP - f2);
               // float d = (Mathf.Abs(fP - f2) + 720) % 360;
             //   if (d< maxCanFireDegree||Mathf.Abs(d-360)<maxCanFireDegree)
                {
                    ac.SetTrigger("attLayer");
                }
            }
            else
            {
                ac.SetTrigger("attLayer");
            }
    }
    protected virtual void selfAdjust_mouse1()
    {
        ac.SetFloat("attTime", 0.1f);

    }
    protected virtual void selfAdjust_mouse0()
    {

    }
    protected virtual void selfAdjust_mouse1Not()
    {
        //ac.SetFloat("attTime", 0.1f);
        

    }
    protected virtual void selfAdjust_mouse0Not()
    {

    }
   
}
