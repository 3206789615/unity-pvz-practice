using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSlider : MonoBehaviour
{
    public Image fillImage;          // 绿色进度条（有 fillAmount）
    public RectTransform head;       // 僵尸头
    public RectTransform fillRect;   // 绿色条的父物体（背景）
    public float speed = 0.0243902f;
    public bool isstart = false;
    void Update()
    {
        float width = fillRect.rect.width;

        float x = -width * fillImage.fillAmount;

        head.anchoredPosition = new Vector2(x, head.anchoredPosition.y);
       
        if (isstart)
        {
            fillImage.fillAmount += speed * Time.deltaTime;            
        }


    }
}
