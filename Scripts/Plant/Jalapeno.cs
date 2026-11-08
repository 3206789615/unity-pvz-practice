using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

  
public class Jalapeno : Plant
{
  
    public GameObject fire;
    private bool fireopen=true;
    public float fireX;
   
    protected override void EnableUpdate()
    {       
        Invoke(nameof(BurnRow), 0.7f);
        Destroy(gameObject, 0.8f); // 稍微延迟销毁，方便你以后加特效
       
    }

    void BurnRow()
    {
       if(fireopen)
        {
            Bornfire();
            fireopen = false;
        }
        // 获取当前所在行的y坐标
        float rowY = transform.position.y;

        // 找到所有僵尸
        Zombie[] zombies = FindObjectsOfType<Zombie>();

        foreach (var z in zombies)
        {
            // 判断是不是同一行（允许一点误差）
            if (Mathf.Abs(z.transform.position.y - rowY) < 0.8f)
            {
                Animator anim = z.GetComponent<Animator>();
                anim.SetTrigger("Boom");
                z.GetComponent<Zombie>().Dead();
            }
        }
    }

    void Bornfire()
    {
        AudioManager.Instance.Play("jalapeno");
        Vector3 firepos = new Vector3(fireX, transform.position.y-0.4f, 0);
        GameObject fires = Instantiate(fire, firepos, Quaternion.identity);
        Destroy(fires, 0.9f);
    }
}
  