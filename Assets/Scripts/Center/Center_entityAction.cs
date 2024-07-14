using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_objectsAction: CenterBase<Center_objectsAction>
{
    public List<Entity> allEntities = new List<Entity>();
    public Action<Entity,int> ac_entityAttack = (a, b) => { };
    public Action<Entity> ac_entityBirch = (a) => { };
    public Action<Entity, int> ac_helper = (a, b) => { };
    [SerializeField] AudioData swordAttackAudioData;
    [SerializeField] AudioData arrowAttackAudioData;
    public override void onEnable_()
    {
        base.onEnable_();
        Center_objectsAction.Instance.ac_entityAttack += simple_entityAttack;
        Center_objectsAction.Instance.ac_entityBirch += simple_entityBirch;
        Center_objectsAction.Instance.ac_helper += simple_helper;
    }
    public override void onDisable_()
    {
        base.onEnable_();
        Center_objectsAction.Instance.ac_entityAttack -= simple_entityAttack;
        Center_objectsAction.Instance. ac_entityBirch-= simple_entityBirch;
        Center_objectsAction.Instance.ac_helper -= simple_helper;
    }
    public void simple_entityAttack(Entity en,int type = 0)
    {
        if (type == Weapon.WeaponType.ju.GetHashCode())
        {
            //近战攻击
            Debug.Log("有人近战攻击");
            AudioManager.Instance.PlayerRandomSFX(swordAttackAudioData);
            if (en.isPlayer)
            {
                //是玩家攻击
                Debug.Log("玩家近战");
            }
            else
            {
                //非玩家攻击
                Debug.Log("非玩家近战");

            }
        }else if(type == Weapon.WeaponType.gong.GetHashCode())
        {

            //远程攻击
            AudioManager.Instance.PlayerRandomSFX(arrowAttackAudioData);
            Debug.Log("有人远程攻击");
            if (en.isPlayer)
            {
                //是玩家攻击
                Debug.Log("玩家远程");
            }
            else
            {
                //非玩家被打
                Debug.Log("非玩家远程");

            }
        }
       
    }
    public void simple_entityBirch(Entity en)
    {
        if (en.isPlayer)
        {
            Debug.Log("玩家生成");
            //是玩家生成
        }
        else
        {
            //非玩家生成
            Debug.Log("非玩家生成");
            allEntities.Add(en);
        }
    }
    public void simple_helper(Entity en,int what)
    {
        if (en.isPlayer)
        {
            Debug.Log("备用");
            //是玩家死
        }
        else
        {
            //非玩家死
            Debug.Log("备用");
        }
    }
    public void setAllEntitiesActiveFalse()
    {
        foreach(var i in allEntities)
        {
            i.isActive = false;
            i.myAm.speed = 0;
        }
    }
    public override void update_()
    {
        base.update_();
        if (Input.GetKeyDown(KeyCode.P)&& Input.GetKey(KeyCode.O)) {
            setAllEntitiesActiveFalse();
        }
    }
}

