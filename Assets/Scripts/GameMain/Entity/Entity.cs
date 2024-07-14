using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : GameBehavior
{
    
    
    public LayerMask entityLayer;//ʵ���
    public string camp;//��Ӫ
    [SerializeField ]
    private float health;//������װ
    public float healthOr=100;
    public float defendValue=1;
    public defendWayType myDefendWay;
    public bool hasDie=false;//������
    public bool selfJudgeDie=true;//
    public bool test_dieImmiditly;
    private float hasUnselectableTime;//����ѡ��ʱ��
    public float defaultUnselectableTime = 0.5f;//Ĭ�ϲ���ѡ��ʱ��
    public float defailtDieToDesTime_ = 5f;

    public Action<Entity,float ,float /*����ʱ��*/> AC_healthDamage=(a,b,c)=> { };
    public Action nothing = () => { };


    //keycon������,��ֵ��awakepre
    //[hidden]public KeyCon keycon;
    //awakePre:1. keycon=getCom<>();2.if(keycon!=null)keycon.attaching(this)else debug.logwarn;
    //
    [HideInInspector] public Animator myAm;
    [HideInInspector]public  Move myMove;
    [HideInInspector] public Weapon  myWeapon;
    [HideInInspector] public Attack myAttack;
    public override void onEnable_()
    {
        base.onEnable_();
        health = healthOr;
        AC_healthDamage+= doAction_selfJudgeDie;
    }
    public override void onDisable_()
    {
        base.onDisable_();
        AC_healthDamage -= doAction_selfJudgeDie;
    }
    // Start is called before the first frame update
    public override void awake_()
    {
        base.awake_();
        //������
        myAm = GetComponentInChildren<Animator>();
        if (myAm != null) nothing();

        //�ƶ�
        myMove = GetComponentInChildren<Move>();
        if (myMove == null) myMove = GetComponentInParent<Move>();
        if (myMove != null) myMove.setEntity(this);
        //����
        myWeapon= GetComponentInChildren<Weapon>();
        if (myWeapon == null) myWeapon = GetComponentInParent<Weapon>();
        if (myWeapon != null) myWeapon.setEntity(this);
        //����
        myAttack = GetComponentInChildren<Attack>();
        if (myAttack == null)myAttack= GetComponentInParent<Attack>();
        if (myAttack != null) myAttack.setEntity(this);

       

    }
    public override void start_()
    {
        base.start_();
        if (AsPlayer.Instance.isNowUseSinglePlayerAndGetState)
        {
            if (tag.Equals("Camp0"))
            {
                isPlayer = true;
            }
            else if (tag.Equals("Camp1"))
            {
                //�޳�
                Collider2D[] c2s = GetComponentsInChildren<Collider2D>();
                foreach (var c in c2s)
                {
                    c.excludeLayers = Center_GameData.Instance.layers.layer_ring;
                }
            }
        }
        Center_objectsAction.Instance.ac_entityBirch(this);
    }
    private void damageHealth(Entity e,float f,float f2=0)
    {
        setHealth(health - deFendWayStore.Instance.do_pickWay(myDefendWay).do_way(e,this,f));
        AC_healthDamage(e, f, f2);
        Center_storeObjects.Instance.ac_entityBeHurt(this, f, e);
    }
    public void doAction_selfJudgeDie(Entity e, float f, float f2 = 0)
    {
        if (selfJudgeDie)
        {
            if (health <= 0)
            {
                do_Die();
                isActive = false;
                if (myMove.rd != null) myMove.rd.simulated = false;
                 sT("die");
                Center_storeObjects.Instance.ac_entityDie(this, e);
                
            }
        }
    }
    private  void do_Die()
    {
        hasDie = true;
        if (test_dieImmiditly)
        {
            do_Des();
        }
    }
    public void damageByValue(Entity e,float f,float f2 = 0)
    {
        if(isActive) damageHealth(e, f, f2);
    }
    public void setHealth(float f){health = f;}
    public float getHealth() { return health; }
    public float useHealth(float f = -1)
    {
        if (f != -1) setHealth(f);
        return health;
    }
    public override void update_()
    {
        base.update_();
        if (isActive)
        {

            if(myMove!=null) myMove.beActive();
            if(myWeapon!=null) myWeapon.beActive();
            if(myAttack!=null) myAttack.beActive();
        }
        mudule_testDieToDes();
    }
    public void mudule_testDieToDes()
    {
        if (hasDie)
        {
            defailtDieToDesTime_ -= Time.deltaTime;
            if (defailtDieToDesTime_ < 0)
            {
                do_Des();
            }
        }
    }
    public float sF(string name,float value=-1) {
        if (myAm == null) { Debug.LogWarning("û�ж�����"); return 0; }
            if (value != -1) myAm.SetFloat(name, value);
            return myAm.GetFloat(name);
    }
    public bool  sB_set(string name, bool  value = true)
    {
        if (myAm == null) { Debug.LogWarning("û�ж�����"); return value; }
        myAm.SetBool(name, value);
        return value;
    }
    public bool sB_get(string name)
    {
        if (myAm == null) { Debug.LogWarning("û�ж�����"); return false; }
        return myAm.GetBool(name);
    }
    public bool sT(string name, bool value = true)
    {
        if (myAm == null) { Debug.LogWarning("û�ж�����"); return value; }
        if (value) myAm.SetTrigger(name);

        else myAm.ResetTrigger(name);
        return value;
    }
}

