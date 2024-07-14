using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wea_contrast : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pack_con(collision);
      //  Debug.Log("01");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        pack_con(collision.collider);
        Debug.Log("02");
    }
    private void pack_con(Collider2D collision)
    {
      
        if (!collision.tag .Equals( tag))
        {
            if (collision.gameObject.layer == 6)
            {
                Debug.Log("碰到飞行无了");
                tri_Flying(collision.gameObject);
            }
            Debug.Log("武器触碰" + collision.gameObject.layer);
        }
    }
    public virtual void tri_Flying(GameObject g)
    {
        Rigidbody2D rd = g.GetComponent<Rigidbody2D>();
        if (rd != null)
        {
            rd.velocity = -rd.velocity*0.8f+Random .insideUnitCircle*rd.velocity /7;
            rd.gravityScale = 3;
            g.GetComponent<Collider2D>().tag = this.tag;
        }
    }
}
