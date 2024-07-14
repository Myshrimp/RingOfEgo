using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
    private float gameTime = 0f;    
    [SerializeField,Range(0f,1f)] float bulletTimeScale = 0.1f;//子弹时间刻度
    float defaultFixedDeltaTime;
    float timeScaleBeforePause;
    float t;

    protected override void Awake()
    {
        base.Awake();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        
        if (!Mathf.Approximately(Time.timeScale, 1f))
        {
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }

    public float GetGameTime()
    {
        return gameTime;
    }
    
    
    public void SlowIn(float duration)
    {
        StartCoroutine(SlowInCoroutine(duration));
    }
    
    public void SlowOut(float duration)
    {
        StartCoroutine(SlowOutCoroutine(duration));
    }
    

    //缓慢将时间刻度减少
    IEnumerator SlowInCoroutine(float duration)
    {
        t = 0f;
        while (t < 1f)
        {
            // if(GameManager.GameState != GameState.Paused)
            // {
                t += Time.unscaledDeltaTime / duration;
                Time.timeScale = Mathf.Lerp(1f, bulletTimeScale, t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            // }

            yield return null;
        }
    }
    //缓慢将时间刻度恢复
    IEnumerator SlowOutCoroutine(float duration)
    {
        t = 0f;
        while(t < 1f)
        {
            // if(GameManager.GameState != GameState.Paused)
            // {
                t += Time.unscaledDeltaTime / duration;
                Time.timeScale = Mathf.Lerp(bulletTimeScale, 1f, t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            // }

            yield return null;
        }
    }
}
