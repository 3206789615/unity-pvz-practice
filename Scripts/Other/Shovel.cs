using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shovel : MonoBehaviour
{
    private CardListUI manager;
    private bool canClick = false;   
    public void Init(CardListUI m)
    {
        manager = m;
        //Debug.Log("manager赋值成功：" + manager);

    }

    void Start()
    {
        Invoke(nameof(EnableClick), 0.1f);
    }

    void EnableClick()
    {
        canClick = true;
    }

    void Update()
    {
        FollowMouse();

        // 点击检测
        if (Input.GetMouseButtonDown(0)&&canClick)
        {            
            CheckPlant();
        }
    }

    void FollowMouse()
    {
        if (Camera.main == null) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    void CheckPlant()
    {
        Collider2D[] cols = Physics2D.OverlapPointAll(transform.position);

        foreach (var col in cols)
        {
            if (col.CompareTag("Plant"))
            {
                AudioManager.Instance.Play("shovel");
                Destroy(col.gameObject);
                break;
            }
        }       
        DestroySelf();       
    }

    void DestroySelf()
    {
        if (manager != null)
            manager.ClearShovel();
        Destroy(gameObject);
    }
}
