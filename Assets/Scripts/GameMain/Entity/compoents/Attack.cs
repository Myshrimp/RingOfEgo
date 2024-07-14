using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static Weapon;

public class Attack : ComAddition
{
    //自动攻击？
    public bool useFire = true;
    [SerializeField]
    public Att_Fire myFire;
   
    public void abbs()
    {

    }
    public override void beActive()
    {
        base.beActive();
        if (myFire.me == null)
        {
            myFire.me = this;
        }
        if (useFire) myFire.update_();
    }
   
    public override void update_()
    {
        if (entity == null)
        {
           if(useFire) beActive();
        }
        base.update_();
       
    }
    public void transOnly_Fire()
    {
        if (useFire)
        {
            myFire.do_fire();
        }
    }

}
[Serializable]
public class Att_Fire
{
    public Attack me;
    public bool willSelf = false;
    public GameObject use;
    public float fireSpeed = 6;
    private float time_hasGo;
    public float time_cd = 3;
    public float num_ = 3;
    public float fireFaceTo;
    [SerializeField]
    public Att_FirePoint myFirePoint;
    [SerializeField]
    public bool useOffset = true;
    public fireOffset myFireOffset;

    //

    public void update_()
    {
        if (myFirePoint.me == null)
        {
            myFirePoint.me = this;
        }
        myFirePoint.update_();
        //角度自适应

        if (myFirePoint.firePointer == null)
        {
            if(me.entity!=null)
            fireFaceTo = me.entity.transform.rotation.z;
            else
                fireFaceTo = me.transform.rotation.z;
        }
        else
        {
            fireFaceTo = myFirePoint.firePointer.eulerAngles.z;
        }
        //自发射
        module_selfFire();
    }
    public void module_selfFire()
    {
        if (!willSelf) return;
        time_hasGo += Time.deltaTime;
        if (time_hasGo > time_cd)
        {
            //fire
            time_hasGo = 0;
            do_fire();
        }
    }
    public virtual void do_fire()
    {
        if (use != null)
        {
            
            Center_objectsAction.Instance.ac_entityAttack(me.entity, WeaponType.gong.GetHashCode());
            for (int i = 0; i < num_; i++)
            {
                float deOffset = myFireOffset.getDeDif();
                Vector3 ro = getFirePointRo(deOffset);
                GameObject g =GameObject. Instantiate(use,getFirePointPos(),Quaternion.Euler(ro),Center_storeObjects.Instance.transform );
                g.tag = me.tag;Debug.Log("强制更改" + me.tag);
                object_tri tri = g.GetComponent<object_tri>();
                if (tri!=null)
                {
                    do_setTriByAttack(tri,me);
                    Debug.Log("找到了");
                }
                Rigidbody2D rd = g.GetComponent<Rigidbody2D>();
                if (rd != null)
                {
                    float vx = MathF.Cos(ro.z * UnityEngine.Mathf.Deg2Rad);
                    float vy = MathF.Sin(ro.z * UnityEngine.Mathf.Deg2Rad);
                //    Debug.Log(ro.z+"xy值"+vx+""+vy);
                    rd.velocity =new Vector2(MathF.Cos(ro.z*UnityEngine.Mathf.Deg2Rad),MathF.Sin(ro.z*UnityEngine.Mathf.Deg2Rad)) * fireSpeed; // Random.insideUnitCircle * 10;
                }
            }
        }
        
    }
    public void do_setTriByAttack(object_tri ob,Attack mine)
    {
        if (!mine.entity.isPlayer)
        {
            ob.willExSelf = Center_GameData.Instance.layers.layer_ring;
            Debug.Log("ring层");
        }
    }
    public Vector2 getFirePointPos()
    {
        if (myFirePoint.fireFrom != null) return myFirePoint.fireFrom.position;
        if (myFirePoint.firePointer != null) return myFirePoint.firePointer.position;
        return me.transform.position;
    }
    public Vector3 getFirePointRo(float de)
    {
        float roZ = 0;
        if (myFirePoint.usePointerDegree)
        {
           roZ= myFirePoint.getRoZ();
           // Debug.Log("获得了myFirePoint的角度"+roZ);
           
        }else
        {
            roZ = me.transform.rotation.z;
        }
        roZ += de;
        //if (myFirePoint.fireFrom != null) return myFirePoint.fireFrom.position;
        //if (myFirePoint.firePointer != null) return myFirePoint.firePointer.position;
        return new Vector3(0, 0, roZ);
    }
}
[Serializable ]
public class Att_FirePoint
{
    [NonSerialized]
    public Att_Fire me;
    public Transform firePointer;
    public Transform fireFrom;
    public Vector2 fireTo;
    public bool willAlwaysToPlayer = true;
    public bool usePointerDegree=true;
    public void update_()
    {
        if (willAlwaysToPlayer)
        {
            fireTo = AsPlayer.Instance.player.transform.position;

            Debug.Log("更新目的地"+fireTo);
            if (me.me.entity.isPlayer)
            {
                willAlwaysToPlayer = false;
            }
        }
    }
    public float getRoZ()
    {
        if (firePointer != null)
        {

            return firePointer.rotation.eulerAngles.z;
        }
        //无心
        if (willAlwaysToPlayer)
        {
        }
            Vector2 v = fireTo;
            if (me == null) Debug.Log("me时NULL");
            else if (me.me == null) Debug.Log("MEME是NULL");
            Vector2 v2 = me.me.transform.position;
            Debug.Log("目的地"+v);
            Debug.Log("qishi地" + v2);
            float f= Mathf.Atan2(v.y - v2.y, v.x - v2.x) * Mathf.Rad2Deg;
            Debug.Log("发射角度"+f);
            return f;
        
        Debug.LogWarning("并没有分配发射点");
        return 0;
    }
    public void setRoZByPoint(float x,float y)
    {
        fireTo = new Vector2(x, y);
        Vector2 v2;
        if (firePointer == null) return;
        if (fireFrom != null)
        {
            v2 = fireFrom.position;
        }else
        {
            v2 = firePointer.position;
        }

        float degree = Mathf.Atan2(y - v2.y, x - v2.x)*Mathf.Rad2Deg;
        firePointer.rotation = Quaternion.Euler(0, 0, degree);
    }
}
[Serializable ]
public class fireOffset
{
    public float deMin=-30, deMax=30,timeOffset=0;
    public float getDeDif()
    {
        return UnityEngine.Random.Range(deMin, deMax);
    }
    public float getTimeOffset()
    {
        return UnityEngine.Random.Range(0,timeOffset);
    }
}