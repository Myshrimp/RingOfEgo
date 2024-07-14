using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ways
{
    public class Way
    {

    }

    public class WayStore<O,T, W> where O :WayStore<O,T,W>
    {
        public Dictionary<T, W> ways=new Dictionary<T, W>();
        public W defalutWay;
     //   public static O<string, T, W> 
       // public static WayStore<T, W> Instance;
        public static O Instance;
        public void do_InitInstance()
        {
            if (Instance == null)
            {
                Instance = this as O;
                do_InitWays();
            }
            else
            {
                Debug.LogWarning("÷ÿ∏¥≥ı ºªØ");
            }
        }
        public virtual void do_InitWays()
        {

        }
        public virtual W do_pickWay(T type)
        {
            if (ways.ContainsKey(type)) return ways[type];
            if (defalutWay != null) return defalutWay;
            return default(W);
        }
        protected void addWay(T t, W w, bool asDefalut = false)
        {
            ways.Add(t, w);
            if (asDefalut) defalutWay = w;
        }
    }
    public enum WayType
    {

    }
}