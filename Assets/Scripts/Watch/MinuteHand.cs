using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHand : MonoBehaviour
{
    public static MinuteHand instance;
    // Start is called before the first frame update
    public float default_cooldown = 20f;
    public float current_cooldown = 0f;
    public Action AbilityGained;
    private Vector3 rotation;
    private float timer = 0f;

    private void Start()
    {
        rotation.z = -6;
        rotation.y = 0;
        rotation.x = 0;
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= current_cooldown)
        {
            AbilityGained?.Invoke();
            timer = 0f;
        }
        
        transform.Rotate(rotation * Time.deltaTime * (60f/current_cooldown));
    }

   
}
