using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firer : MonoBehaviour
{
    public bool willSelf = true;
    public float hasGo;
    public float time_cd;
    public float num_=1;

    public GameObject use;
    public bool isUseFaceByRot=true;

    public Vector2 facePoint;


    public float speed = 50;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform t = transform;
        facePoint = transform.position;
        if (firePoint != null) { t = firePoint; facePoint = firePoint.position; }
        //if (isUseFaceByRot) faceTo = t.right.normalized;
      //  Debug.Log(t.right);
        
        if (!willSelf) return;
        hasGo += Time.deltaTime;
        if(hasGo>time_cd)
        {
            do_fire();
            hasGo = 0;
        }
    }
    public void do_fire()
    {
        if (use != null)
        {
            for (int i = 0; i < num_; i++)
            {
                GameObject g = Instantiate(use, facePoint , Quaternion.Euler(0, 0, 0/*Mathf.Atan2( /*faceTo.x))*/));
                Rigidbody2D rd = g.GetComponent<Rigidbody2D>();
                if (rd != null)
                {
                  //  rd.velocity = faceTo * speed + Random.insideUnitCircle * 10;
                }
            }
        }
    }
}
