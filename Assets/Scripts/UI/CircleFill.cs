using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFill : MonoBehaviour
{
    public Image circleImage; // 圆形Image组件
    private float targetFillAmount; // 目标填充量

    void Start()
    {
        if (circleImage != null)
        {
            circleImage.fillAmount = 0f; // 初始化填充量为0
            targetFillAmount = 0f;
        }
    }

    // 调用此方法来增加填充量

    public void SetFillAmount(float amount)
    {
        targetFillAmount = Mathf.Clamp( amount, 0f, 1f); // 确保填充量在0到1之间
        circleImage.fillAmount = targetFillAmount;
    }

}
