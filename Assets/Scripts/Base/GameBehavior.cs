using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public bool isAdmittedUpdate = true;
    [HideInInspector]
    public Entity entity;
    public bool isActive = true;
    public bool isKeyCon = true;
    public bool isPlayer;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    /// pu 
    /// 
    public string tagWillGet = "";
    public GameBehavior  setEntity(Entity e=default )
    {
        entity = e;
        if(e==default)
        {
            entity = GetComponentInParent<Entity>();
            if (entity == null) entity = GetComponentInChildren<Entity>();
            if (entity = null) Debug.Log("完全没有找到实体啊");
            else
            {
                tag = entity.tag;
            }
        }
        return this;
    }
    public void OnEnable()
    {
        onEnable_pre();
        onEnable_();
        onEnable_post();
    }
    public void OnDisable()
    {
        onDisable_pre();
        onDisable_();
        onDIsable_post();
    }
    public void Awake()
    {
        awake_pre();
        awake_();
        awake_post();
    }
    // Start is called before the first frame update
    public  void Start()
    {
        start_pre();
        start_();
        start_post();
    }

    // Update is called once per frame
    public  void Update()
    {
        
        update_pre();
        if(isAdmittedUpdate) update_();
        update_post();
    }
    public virtual void awake_pre() { }
    public virtual void awake_() { }
    public virtual void awake_post() {
    
    }
    public virtual void start_pre() { }
    public virtual void start_() { }
    public virtual void start_post() { }
    public virtual void update_pre() { }
    public virtual void update_() { }
    public virtual void update_post() { }
    public virtual void onEnable_pre() { }
    public virtual void onEnable_() { }
    public virtual void onEnable_post() { }
    public virtual void onDisable_pre() { }
    public virtual void onDisable_() { }
    public virtual void onDIsable_post() { }
    public virtual void board_prepare01() { }
    public virtual void board_prepare02() { }
    public virtual void board_prepare03() { }
    public virtual void board_prepare04() { }
    public virtual void board_prepare05() { }
    public virtual void board_prepare06() { }
    public virtual void board_update01() { }
    public virtual void board_update02() { }
    public virtual void board_update03() { }
    public virtual void board_update04() { }
    public virtual void board_update05() { }
    public virtual void board_update06() { }
    public virtual void do_Des() { Destroy(gameObject); }
    public virtual void do_ReBack() { }
    public virtual void do_Birch() { }
    public bool  isInLayer(LayerMask ly)
    {
        if (Mathf.Pow(2, gameObject.layer) == ly.value) return true;
       return false;
    }
    public static  bool isObjectInLayer(GameObject g,LayerMask ly)
    {
     //   Debug.Log("区别" + Mathf.Pow(2, g.layer) + ly.value);
        if (Mathf.Pow(2, g.layer ) == ly.value) return true;
        return false;
    }
}
