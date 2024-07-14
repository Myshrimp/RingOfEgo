using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conPlayer : MonoBehaviour
{
    public float speed = 30;
    private Animator ac;
    public bool HardConCanBeCon;
    public float fx, fy,faceTo;
    public bool isSlider;
    public float banSlider;
   

    private Rigidbody2D rd;
   
    public  bool isJumping;
    public LayerMask wall;
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<Animator>();
        
        rd = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        module_sliderIn();
        module_jump();
        if (!HardConCanBeCon)
        {
             fx = Input.GetAxis("Horizontal");
             fy = Input.GetAxis("Vertical");
            transform.position += Time.deltaTime * speed * new Vector3(fx, fy);
            if (fx != 0)
            {
                transform.rotation = Quaternion.Euler(0, faceTo = fx > 0 ? 0 : 180, 0);//方向判断
               
            }
            ac.SetBool("isMoving", fx != 0 || fy != 0);//有非零，移动
        }
        else
        {
            if (isSlider)
            {
                mudule_slider();
            }
        }
        
       

       
        module_speedCon();
       
      //  module_attKan();
    }
    public void module_jump()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("按下L");
          
           
            if (!isJumping&& rd.IsTouchingLayers(wall.value)){
                Debug.Log("给力");
                isJumping = true;
                rd.velocityY += 40;
                ac.SetTrigger("jumpStart");
            }
        }
    }
    public void module_sliderIn()
    {
        banSlider -= Time.deltaTime;
        if (Input .GetKeyDown(KeyCode.K))
        {
            startSlider();
        }
    }
    public void mudule_slider()
    {
        float f;
        f = ac.GetFloat("slidTime");
        if (f > 0) transform.position += speed * 2.5f * Time.deltaTime * (fx != 0 || fy != 0 ? new Vector3(fx, fy, 0).normalized : new Vector3(Mathf.Cos(faceTo * Mathf.Deg2Rad), 0, 0).normalized);
        else
        {
            isSlider = false;
            HardConCanBeCon = false;
            banSlider = 1.5f;
        }
    }
    public void module_speedCon()
    {
        speed = 30;
        if(Input.GetKey(KeyCode.Space))
        {
            speed *= 2;
            
        }else
        {

        }
        ac.SetFloat("runScale", speed / 30);
    }
   
    public void startSlider()
    {
        if (banSlider > 0)
        {
            return;
        }
        if (!isSlider)
        {
            isSlider = true;
            HardConCanBeCon = true;
            ac.SetFloat("slidTime", 2);
            ac.SetTrigger("slidStart");
          
        }
       
    }
    public void endSlider()
    {
        isSlider = false;
        HardConCanBeCon = false;
        banSlider = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if(isJumping)
            {
                isJumping = false;
                ac.SetTrigger("jumpEnd");
            }
        }
    }
}
