using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OuterCircle : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public static OuterCircle instance;
    public bool mouseEntered = true;

    private void Awake()
    {
        instance = this;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEntered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEntered=false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
