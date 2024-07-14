using UnityEngine;

public class flyingObject : MonoBehaviour
{

    public bool isRoToV;
    public float liveTime = 4;
    private Rigidbody2D rd2;
    
    public Vector2 vP;

    public bool isCanRelay;
    public GameObject nullRelayOn;

    public bool isHasBoom;
    public GameObject useBoom;
    // Start is called before the first frame update
   public   void Start()
    {
        start_();
    }
    public virtual void start_()
    {
        rd2 = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public  void Update()
    {
        update_();
        liveTime -= Time.deltaTime;
        if(liveTime < 0)
        {
            
        }
    }
    public virtual void update_()
    {
        if (isRoToV) do_roToV();
    }
    public virtual void do_roToV()
    {
        float ro = Mathf.Atan2(rd2.velocityY, rd2.velocityX)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,ro);
       // Debug.Log(ro);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       vP= collision.ClosestPoint(transform.position);
        if(collision.tag.Equals(this.tag))
        {
            tri_selfTag(collision .gameObject );
        }else {
            tri_otherTag(collision .gameObject );
           // Debug.Log("触碰层" + collision.gameObject.layer);
            if(collision.gameObject.layer == 7)
            {
                tri_otherEntity(collision.gameObject );
            }
        }
            
    }
    public virtual void tri_selfTag(GameObject g)
    {
      // Debug.Log("同便签");
    }
    public virtual void tri_otherTag(GameObject g)
    {
        //Debug.Log("不同便签");
        do_des( g);
    }
    public virtual void tri_otherEntity(GameObject g)
    {
       // Debug.Log("不同实体");
        if (isCanRelay)
        {
           GameObject gg=  Instantiate(nullRelayOn, transform.position, transform.rotation, g.transform );
            Vector3 or = gg.transform.localScale;
            gg.transform.localScale = or / gg.transform.parent.transform.lossyScale.x;
        }
    }
    public virtual void do_des(GameObject g)
    {
        if (isHasBoom)
        {
            if (g.layer != 8)//唯独武器不消失
            {
                GameObject gg= Instantiate(useBoom, vP, Quaternion.identity);
               
                Destroy(gameObject);
            }
        }
      
    }
}
