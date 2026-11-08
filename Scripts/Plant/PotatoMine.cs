using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PotatoMine : Plant
{

    public  SpriteRenderer maidipatato;
    public  SpriteRenderer potatoMine;
    public  SpriteRenderer Boompatato;

    private Transform target;
    public LayerMask zombieLayer;
    Vector2 boxSize = new Vector2(2f, 0.8f);
    private bool potatoonetime=true;
    private bool hasAttacked = false;
    private void Awake()
    {     
       
    }
    protected override void DisableUpdate()
    {
        base.DisableUpdate();
    }
    protected override void EnableUpdate()
    {      
        base.EnableUpdate();

        if (!hasAttacked&&!potatoonetime)
        {
            Invoke(nameof(DetectZombie), 1f);
        }
        if (potatoonetime)
        {
            potatoMine.enabled = false;
            maidipatato.enabled = true;
            Invoke(nameof(BornPotato), 13f);            
        }
    }

    void BornPotato()
    {
        potatoMine.enabled = true;
        maidipatato.enabled = false;
        potatoonetime = false;
    }

    void DetectZombie()
    {
        // 检测前后
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            boxSize,
            0,
            zombieLayer
        );

        if (hits.Length == 0) return;

        // 找最近的僵尸
        float minDis = float.MaxValue;
        Transform closest = null;

        foreach (var hit in hits)
        {
            float dis = Vector2.Distance(transform.position, hit.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                closest = hit.transform;
            }
        }

        if (closest != null)
        {
            target = closest;
            hasAttacked = true;
            potatoattack();
        }                 
    }

    void potatoattack()
    {
        potatoMine.enabled = false;
        maidipatato.enabled = false;
        Boompatato.enabled = true;
        AudioManager.Instance.Play("potato_mine");
        Collider2D[] hits = Physics2D.OverlapBoxAll(
           transform.position,
           boxSize,
           0,
           zombieLayer
       );
        foreach (var hit in hits)
        {
            hit.GetComponent<Zombie>().Takedamage(900);
        }

        Destroy(gameObject, 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}