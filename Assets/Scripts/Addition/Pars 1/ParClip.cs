using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParClip 
{
    private ParClip_c container;
    [Header("������")]
    public bool isUseful = true;
    public bool isControlledUpdating = false ;
    public bool isRelyOnPs=true;
    public ParticleSystem source_PS;
    [Header("������ʱ������")]
    public Color color;
    public bool awake;
    public bool loop;
    public Sprite useSprite;
    public float size = 1;
    public float launchNumByTime = 10;
    public bool useTail;
    public Color tailColor;
    
    // Start is called before the first frame update
    public void do_StartByClip(ParClip_c c)
    {
         container = c;
         if (!isRelyOnPs) return;
        setPS();
            if (source_PS == null)
            {
                Debug.LogWarning("ΪʲĪ������Դ����û������ϵͳ�Ķ����ǾͰ��������ӹ��˰�");
                isRelyOnPs = false;
            }else
        {
            //��������ʹ�õ�
            if(color.a==0) color = tailColor = Color.white;
            do_apply();
        }
        
    }
    private void setPS()
    {
        setPS(container.gameObject);
    }
    public void setPS(GameObject g)
    {
        source_PS = g.GetComponent<ParticleSystem>();
    }
    public void do_apply(ParticleSystem ps, bool first = false)
    {
        if (ps == null || !isUseful||!isRelyOnPs) return;
        Debug.Log("Ӧ������");
    }
    
    public void do_apply()
    {
        do_apply(source_PS);
    }
    public void do_Play()
    {
        if(isUseful&& isRelyOnPs)
        {
            source_PS.gameObject.SetActive(false);
        }
    }
    public void do_Stop()
    {
        if (isUseful && isRelyOnPs)
        {
            Debug.Log("�ر���һ������");
            if (source_PS == null)
            {
                Debug.Log("�׵£�");
                return;
            }
            source_PS.gameObject.SetActive(false);
        }
    }
}
