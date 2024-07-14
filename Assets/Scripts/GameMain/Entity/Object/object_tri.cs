using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class object_tri : GameBehavior
{
    public bool isFoeverConTrigger;//���������ײ
    public bool willTriggerByActivingTimes;//������active�����ã������ƴ�����ʱ��
    public bool isActiving = true;//����ײ
    public bool willEscapeAwakeTrigger = true;
    public bool willDieAfterTrigger = true;
    public Rigidbody2D rd;
    private Collider2D myCol;
    [HideInInspector]
    public Vector2 tri_Point;
    public bool isUsingAsConsumable = true;
    [SerializeField]
    public objectAsConsumable myAsConsumable;
    public bool isUsingAsWeaponInHand = false;
    [SerializeField]
    public objectAsWeaponInHand myAsWeaponInHand;
    public float hurt = 10;
    public LayerMask willExSelf;
    public void setActiving(bool b = true)
    {
        isActiving = b;
    }
    public override void update_()
    {
        base.update_();
        //active��ԭ
        if (willEscapeAwakeTrigger)
        {
            isActiving = willEscapeAwakeTrigger;
            willEscapeAwakeTrigger = false;
            if (myCol != null) myCol.enabled = true;
        }
        if (isFoeverConTrigger)
        {
            if (myCol != null)
            {
                myCol.enabled = isActiving;

            }
        }
    }
    public override void awake_()
    {
        base.awake_();
        rd = GetComponent<Rigidbody2D>();
        myCol = GetComponent<Collider2D>();
        if (isUsingAsConsumable) myAsConsumable.me = this;
        if (willEscapeAwakeTrigger)
        {
            willEscapeAwakeTrigger = isActiving;
            isActiving = false;
            if (myCol != null) myCol.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //   Debug.Log("trigger");
      // Debug.Log("��¼��+����");
        if (!isActiving||willEscapeAwakeTrigger) return;//���������
        //�ܿ�
        object_tri obj = collision.GetComponent<object_tri>();
        if (obj != null && obj.willEscapeAwakeTrigger) { Debug.Log("��ΪwillTrigger����"); return; };
        if (willTriggerByActivingTimes)
        {
            isActiving = false;
            if(entity!=null) entity.sB_set("AttLayer_isActiving", false);
        }
        tri_Point= collision.ClosestPoint(transform.position);
        if (collision.tag.Equals(this.tag))
        {
            tri_selfTag(collision.gameObject);//�Լ��ҵı�ǩ��Ӫ
        }
        else if(!GameBehavior.isObjectInLayer(collision.gameObject, Center_GameData.Instance.layers.layer_default)
            && !GameBehavior.isObjectInLayer(collision.gameObject, willExSelf))
        {
            tri_otherTag(collision.gameObject);//�����ҵı�ǩ��Ӫ
            /*    Debug.Log("����entityLayer"+ Center_GameData.Instance.layers.id);
                Debug.Log("������" + collision.gameObject.layer);
                Debug.Log("�����涨��" + Center_GameData.Instance.layers.layer_entity.value);
                Debug.Log("�����涨��wall" + Center_GameData.Instance.layers.layer_wall.value);
               */
            //  Debug.Log("����ʵ�����");
         //   Debug.Log("000000׼������ʬ��");
            Debug.Log("lay1"+collision .gameObject.layer+"entity��2����"+ Center_GameData.Instance.layers.layer_entity);
            if (GameBehavior.isObjectInLayer(collision.gameObject ,Center_GameData.Instance.layers.layer_entity))
            {
                Debug.Log("000000���Ե�ʵ��");
             //   Debug.Log("���������");
                tri_otherEntity(collision.gameObject);

            }
        }
    }
    public virtual void tri_selfTag(GameObject g)
    {
        // Debug.Log("ͬ��ǩ");
    }
    public virtual void tri_otherTag(GameObject g)
    {
       Debug.Log("��¼��+������Ǩ"+tag+"___tri___"+g.tag);
        //Debug.Log("��ͬ��ǩ");
        if (willDieAfterTrigger) do_Des();//����
        if (isUsingAsConsumable) myAsConsumable.do_tri_otherTag(g);
    }
    public virtual void tri_otherEntity(GameObject g)
    {
        //Debug.Log("��¼��+����ʵ��");
        // Debug.Log("��ͬʵ��");

        if (isUsingAsConsumable) myAsConsumable.do_tri_entity(g);
        Debug.Log("�ճ��Ϻ�");
        Entity e = g.GetComponentInChildren<Entity>();
        if (e != null) e.damageByValue(GetComponentInChildren<Entity>(), hurt, 0);
    }
}
[Serializable]
public  class objectAsWeaponInHand
{

}
[Serializable]
public class objectAsConsumable
{
    public float liveTime = 4;
    public bool isRoToRdV = true;
    [HideInInspector]
    public object_tri me;
    public bool canRelayOn = true;
    public GameObject nullRelayOnObject;

    public bool isHasBoom = true;
    public GameObject useBoom;
    public void update_()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
        {
            me.do_Des();
           // do_Des();
            //Destroy(gameObject);
        }
        if (isRoToRdV)
        {

            float ro = Mathf.Atan2(me.rd.velocityY, me.rd.velocityX) * Mathf.Rad2Deg;
            me.transform.rotation = Quaternion.Euler(0, 0, ro);
        }
    }
    public void do_tri_entity(GameObject g)
    {
        if (canRelayOn)
        {
            // Debug.Log("Ӧ��relay" +
            //    "");
            if (nullRelayOnObject == null) return;
            // Debug.Log("���relay");
            GameObject gg = GameBehavior.Instantiate(nullRelayOnObject, me.transform.position, me.transform.rotation, g.transform);
            //��С����Ӧ
            Vector3 or = gg.transform.localScale;
            gg.transform.localScale = or / gg.transform.parent.transform.lossyScale.x;
        }
    }
    public void do_tri_otherTag(GameObject g = default)
    {
       
        if (g != null && !GameBehavior.isObjectInLayer(g, Center_GameData.Instance.layers.layer_weapon))
        {
            //Debug.Log("233");
            do_addition_boom(g);
        }

    }
    public void do_addition_boom(GameObject g = default)
    {
        if (isHasBoom)
        {
            GameObject gg = GameBehavior.Instantiate(useBoom, me.tri_Point, Quaternion.identity);
            isHasBoom = false;
        }
    }


    public void do_Des(GameObject g=default ) {
        if (isHasBoom)
        {
       GameObject gg =GameBehavior. Instantiate(useBoom, me.tri_Point, Quaternion.identity);
            isHasBoom = false;
           }
        }
    }

   

