using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static equip;

public class Weapon : ComAddition
{
    public bool isEquiping;
    public GameObject myEquipingWeapon;
    public bool isHardRequireMouse1;

    public float maxCanFireDegree = 60;
    public float  applyFaceVForWeapon;
    public bool isUseMouse = true;

    private object_tri myEquipingWeaTriBehavior;
    
    public enum WeaponType
    {
        ju = 0, gong = 1
    }
    public WeaponType myWeaponType;
    public override void awake_()
    {
        base.awake_();
       
       
    }
    public override void beActive()
    {
        base.beActive();
        module_ValueTrans();
        module_EquipWeapon();
        module_useWeapon();
        module_WeaponValueTrans();

        
    }
    private void module_ValueTrans()
    {
        if (myEquipingWeapon != null)
        {
            myEquipingWeapon.tag = tag;
          
        }
        entity.sF("attTypeX", myWeaponType.GetHashCode());
        entity.sB_set("isConByMouse1", isHardRequireMouse1);
        if(isHardRequireMouse1)  isEquiping = entity.sB_get("isEquiping");
        else entity.sB_set("isEquiping", isEquiping);
    }
    private void module_EquipWeapon()
    {
        if (keyMouse1Down())
        {
            if (isHardRequireMouse1)
            {
                if (isEquiping)
                {
                    entity.sT("attStart", false);
                    entity.sB_set("isEquiping",false);
                    //卸下
                }else
                {
                    entity.sT("attStart");
                }
            }
        }

    }
    public void module_useWeapon(bool b=false)
    {

       // Debug.Log("useWeapon");
        //
        if (!isEquiping&&isHardRequireMouse1) return;
        if (keyMouse0Down()||b)
            if (myWeaponType == WeaponType.gong)
            {
               // float myFace = entity.myMove.faceTo;

               // if (isUseMouse)
               // {
                 //   applyFaceVForWeapon = myFace;
              //  }
                // float f2 = Mathf.Atan2(fire.faceTo.y, fire.faceTo.x) * Mathf.Rad2Deg;
                // //  Debug.Log("fp"+fP);
                //  Debug.Log(fP - f2);
                //  float d = (Mathf.Abs(fP - f2) + 720) % 360;
              //  float degree = MathEv.getDegreeByDegree(myFace, applyFaceVForWeapon);
              //  if (degree < maxCanFireDegree || Mathf.Abs(degree - 360) < maxCanFireDegree)
                {
                    if(entity.isPlayer&&isUseMouse) mudule_addtition_playerFireFace();
                    entity.sT("attLayer");
                    
                }
            }
            else//锯子
            {
                if (myEquipingWeaTriBehavior == null)
                {
                    myEquipingWeaTriBehavior = myEquipingWeapon.GetComponent<object_tri>();
                    if (myEquipingWeaTriBehavior != null)
                    {
                        myEquipingWeaTriBehavior.setEntity(entity);
                    }
                }
              //  Debug.Log(gameObject.name + "使用了攻击+" + myEquipingWeaTriBehavior == null?0:666);
                entity.sT("attLayer");
                Center_objectsAction.Instance.ac_entityAttack(entity, myWeaponType.GetHashCode());
              //  Debug.Log("检测点+攻击开始");
            }
    }
    public void mudule_addtition_playerFireFace()
    {
        Attack ac = entity.myAttack;
        if (ac != null)
        {
            Vector2 v = AsPlayer.Instance.mousePosition;
            ac.myFire.myFirePoint.setRoZByPoint(v.x,v.y);
        }
    }
    public void  module_WeaponValueTrans()
    {
        if (myEquipingWeaTriBehavior != null)
        {
            myEquipingWeaTriBehavior.isActiving = entity.sB_get("AttLayer_isActiving");
            //Debug.Log(gameObject.name + "使用了ISActive");
        }
    }
    // Start is called before the first frame update

}
