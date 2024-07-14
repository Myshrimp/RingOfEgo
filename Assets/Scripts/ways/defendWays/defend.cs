using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ways;

public class defend 
{

}
public class deFendWayStore : WayStore<deFendWayStore , defendWayType, defendWay>
{
    public override void do_InitWays()
    {
        base.do_InitWays();
        addWay(defendWayType.normal, new defendWay_normal(),true);
        addWay(defendWayType.hardDefend, new defendWay_hardDefend());
        addWay(defendWayType.magicDefend, new defendWay_magicDefend ());
    }

}
public enum defendWayType
{
    normal, hardDefend, magicDefend

}
public class defendWay : Way
{
    //
    public Entity from;
    public Entity to;
    public float damageValue;
    public virtual float do_way(Entity from,Entity to,float damageValue)
    {
        return 0;
    }

}
public class defendWay_normal:defendWay 
{
    public override float do_way(Entity from, Entity to, float damageValue)
    {
        return Mathf.Max(1, damageValue - to.defendValue);
        //return base.do_way(e, e2, damageValue);
    }
}
public class defendWay_hardDefend : defendWay
{
    public override float do_way(Entity from, Entity to, float damageValue)
    {
        return Mathf.Max(0, damageValue - to.defendValue);
        //return base.do_way(e, e2, damageValue);
    }
}
public class defendWay_magicDefend : defendWay
{
    public override float do_way(Entity from, Entity to, float damageValue)
    {
        //Ëæ±ãÐ´µÄ
        return Mathf.Max(0, damageValue - to.defendValue);
        //return base.do_way(e, e2, damageValue);
    }
}