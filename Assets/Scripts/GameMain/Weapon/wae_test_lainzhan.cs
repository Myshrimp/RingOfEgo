using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wae_test_lainzhan : MonoBehaviour
{
    private Animator ac;
    public  bool isRandom;
    
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      // if(isRandom ) ac.SetFloat("attP_int", Random.Range(0, 4));
    }
    public void change_int()
    {
        int a = Random.Range(0, 4);
        while (a == ac.GetFloat("attTypeY"))
        {
             a = Random.Range(0, 4);
        }
        ac.SetFloat("attTypeY",a);
    }
    public void ban_att()
    {
        
    }
}
