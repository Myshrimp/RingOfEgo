using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class helper_toMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 v = new Vector2();
        if (Input.GetMouseButton(0))
        {
            v = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
            float ff = Mathf.Atan2(v.y - transform.position.y, v.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, ff);
            Debug.Log(transform .position);
        }
    }
}
