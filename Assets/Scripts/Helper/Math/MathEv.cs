using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class MathEv 
{
  public static float getDegreeByVector2(Vector2 v1,Vector2 v2)
    {
        float dot = Vector2.Dot(v1, v2);
        if (dot == 0) { Debug.LogWarning("µã³ËÎªÁã"); return -1;}
        return Mathf.Acos(Vector2.Dot(v1, v2) / v1.magnitude / v2.magnitude)*Mathf.Rad2Deg;
    }
    public static float getDegreeByFloat(float x1,float y1,float x2,float y2)
    {
        return getDegreeByVector2(new Vector2(x1, y1), new Vector2(x2, y2));
    }
    public static float getDegreeByDegree(float d1,float d2)
    {
        return getDegreeByVector2(new Vector2(Mathf.Cos(d1 * Mathf.Deg2Rad), Mathf.Sin(d1 * Mathf.Deg2Rad)),
           new Vector2(Mathf.Cos(d2* Mathf.Deg2Rad), Mathf.Sin(d2 * Mathf.Deg2Rad)));
    }
}
