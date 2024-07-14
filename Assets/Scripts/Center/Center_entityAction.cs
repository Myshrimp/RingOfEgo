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
            //��ս����
            Debug.Log("���˽�ս����");
            AudioManager.Instance.PlayerRandomSFX(swordAttackAudioData);
            if (en.isPlayer)
            {
                //����ҹ���
                Debug.Log("��ҽ�ս");
            }
            else
            {
                //����ҹ���
                Debug.Log("����ҽ�ս");

            }
        }else if(type == Weapon.WeaponType.gong.GetHashCode())
        {

            //Զ�̹���
            AudioManager.Instance.PlayerRandomSFX(arrowAttackAudioData);
            Debug.Log("����Զ�̹���");
            if (en.isPlayer)
            {
                //����ҹ���
                Debug.Log("���Զ��");
            }
            else
            {
                //����ұ���
                Debug.Log("�����Զ��");

            }
        }
       
    }
    public void simple_entityBirch(Entity en)
    {
        if (en.isPlayer)
        {
            Debug.Log("�������");
            //���������
        }
        else
        {
            //���������
            Debug.Log("���������");
            allEntities.Add(en);
        }
    }
    public void simple_helper(Entity en,int what)
    {
        if (en.isPlayer)
        {
            Debug.Log("����");
            //�������
        }
        else
        {
            //�������
            Debug.Log("����");
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

