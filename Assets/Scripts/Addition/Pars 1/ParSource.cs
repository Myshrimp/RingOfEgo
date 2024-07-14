using EV.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EV
{
    public class ParSource : dataC_GameOb
    {
        
        [Header("默认配置")]
        [SerializeField]
        private ParClip defalutPar;
        public int getSetPointer(int i = -1)
        {
            if (i != -1)
            {
                if (datas.Count > i)
                {
                    if (pointer != i)
                    {

                        switchPars(i);
                        pointer = i;
                    }
                    else
                    {
                        Debug.LogWarning("未更换指示");
                    }
                }
                else
                {
                    Debug.LogWarning("使用指示超出 来自粒子源+" + gameObject.name);
                }
            }
            return pointer;
        }
        public void do_Play()
        {
            do_PlayParByIndex(pointer);
        }
        public void do_PlayParByIndex(int i)
        {
            if (datas.Count > i)
            {
                do_PlayParByGameObject(datas[i].value);
                //  sources[i]单独播放
            }
        }
        public void do_PlayParById(int i)
        {
            foreach (var g in datas)
            {
                if (g.tag.id == i)
                {
                    do_PlayParByGameObject(g.value);
                }
            }
        }
        public void do_PlayParByName(string i)
        {
            foreach (var g in datas)
            {
                if (EV_QuickTool.getStringCompareEasy(i,g.name_))
                {
                    do_PlayParByGameObject(g.value);
                }
                else
                {
                    if (EV_QuickTool.getStringCompareEasy(i, g.tag.name_))
                    {
                        do_PlayParByGameObject(g.value);
                    }else
                    {
                        Debug.LogWarning("完全没找到");
                    }
                }
            }
        }
        private void do_PlayParByGameObject(GameObject g)
        {
            //????
            if (defalutPar != null&&defalutPar.isUseful)
            {
                defalutPar.do_apply(g.GetComponent<ParticleSystem>());
            }
        }
        private void switchPars(int i)
        {
            if(defalutPar!=null&&defalutPar.isUseful)
            nowUse.value.SetActive(false);
            nowUse = datas[i];
            nowUse.value.SetActive(true);
        }
        public override void Start()
        {
            base.Start();
            if (nowUse.value  != null)
            {
                nowUse.value = Instantiate(nowUse.value);
            }
            if(defalutPar !=null&&defalutPar.isUseful)
            {

                do_applyDefalut(nowUse.value);
            }
        }
        public void do_applyDefalut(GameObject g)
        {
            defalutPar.setPS(g);
            defalutPar.do_Stop();
            defalutPar.do_apply();
             defalutPar.do_Play();
        }
    }
    
}