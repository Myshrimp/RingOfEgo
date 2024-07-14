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
            //是玩家被打
            Debug.Log("玩家挨打");
            Ring.instance.scale_speed -= penalty;
            impulse_source.GenerateImpulse(impulse_intensity);

        }else
        {
            //非玩家被打
            Debug.Log("其它被打");
            impulse_source.GenerateImpulse(impulse_intensity * impulse_discount);
        }
    }
    public void simple_entityDie(Entity en, Entity from = default)
    {
        if (en.isPlayer)
        {
            Debug.Log("玩家死");
            //是玩家死
        }
        else
        {
            //非玩家死
            Debug.Log("其他死");
            Ring.instance.scale_speed += bonus;
        }
    }
}
