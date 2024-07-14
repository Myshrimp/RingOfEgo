using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center_storeObjects : CenterBase<Center_storeObjects>
{
    public Action<Entity ,float ,Entity > ac_entityBeHurt=(a,b,c)=> { };
    public Action<Entity, Entity> ac_entityDie = (a, b) => { };
    public float bonus = 0.1f;
    public float penalty = 0.01f;
    public float impulse_intensity = 4f;
    public float impulse_discount = 0.8f;

    public Cinemachine.CinemachineImpulseSource impulse_source;
    public override void onEnable_()
    {
        base.onEnable_();
        Center_storeObjects.Instance.ac_entityBeHurt += simple_entityBeHurt;
        Center_storeObjects.Instance.ac_entityDie += simple_entityDie;
    }
    public override void onDisable_()
    {
        base.onEnable_();
        Center_storeObjects.Instance.ac_entityBeHurt -= simple_entityBeHurt;
        Center_storeObjects.Instance.ac_entityDie -= simple_entityDie;
    }
    public void simple_entityBeHurt(Entity en,float value=0,Entity from=default)
    {
        if (en.isPlayer)
        {
            //����ұ���
            Debug.Log("��Ұ���");
            Ring.instance.scale_speed -= penalty;
            impulse_source.GenerateImpulse(impulse_intensity);

        }else
        {
            //����ұ���
            Debug.Log("��������");
            impulse_source.GenerateImpulse(impulse_intensity * impulse_discount);
        }
    }
    public void simple_entityDie(Entity en, Entity from = default)
    {
        if (en.isPlayer)
        {
            Debug.Log("�����");
            //�������
        }
        else
        {
            //�������
            Debug.Log("������");
            Ring.instance.scale_speed += bonus;
        }
    }
}
