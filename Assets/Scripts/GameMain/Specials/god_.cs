using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class god_ : MonoBehaviour
{
    public float scale=0;
    public bool isDraging;
    public bool isZhuaing;
    public Vector2 maxPos;
    public bool isUseOnPlayer = true;
    public float angeMax;
    public float sizeMax;
    public GameObject hand;
    public GameObject gebo;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        if (isUseOnPlayer)
        {
            col = AsPlayer.Instance.player.gameObject.GetComponent<Collider2D>();
        }
    }
   
    public void start()
    {
        if (isUseOnPlayer)
        {
            Vector2 v = transform.position;
            maxPos = AsPlayer.Instance.player.transform .position;
            angeMax = Mathf.Atan2(maxPos.y - v.y, maxPos.x - v.x)*Mathf.Rad2Deg;
            sizeMax = Vector2.Distance(maxPos, v);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //start();
        if (isUseOnPlayer&&isDraging)
        {
            Quaternion  v = Quaternion.Euler(new Vector3( 0, 0, angeMax));
            float size = sizeMax * scale;
            Debug.Log("max" + sizeMax);
            hand.transform.rotation =gebo.transform .rotation = v;
            hand.transform.position = Vector2.Lerp(transform.position, maxPos, scale);//  Mathf.Lerp(transform .position,)
            gebo.transform.localScale = new Vector3(size, 1, 1);
            if (isZhuaing)
            {
                AsPlayer.Instance.player.transform.position = hand.transform.position;
            }
        }
        
    }
    public void closeTri()
    {
        if (col != null)
        {
            //col.enabled = false;
        }
    }
    public void startTri()
    {
        if (col != null)
        {
            col.enabled = true;
            Debug.Log("triBack");
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }

}
