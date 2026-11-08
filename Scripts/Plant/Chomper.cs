using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 极简大嘴花
public class Chomper : Plant
{
    private Animator animator;
    private Zombie targetZombie;
    private bool isDigesting = false;

    public float attackRange = 2.3f; // 攻击范围
    public float yattckRange = 0.7f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void EnableUpdate()
    {
        base.EnableUpdate();

        if (isDigesting) return;

        // 找最近的僵尸
        targetZombie = FindClosestZombie();

        if (targetZombie != null)
        {          
            Attack();
        }
    }

    void Attack()
    {
        isDigesting = true; // 锁定攻击
        animator.SetTrigger("isAttack");
        OnAttackFinished();
    }

    // Animation Event 在 Attack 动画最后调用
    public void OnAttackFinished()
    {
        if (targetZombie != null)
        {
            Destroy(targetZombie.gameObject); // 直接吃掉僵尸
            targetZombie = null;
        }

       
        StartCoroutine(OnDigestFinished());
    }

    // Animation Event 在 Digest 动画最后调用
    IEnumerator OnDigestFinished()
    {
        yield return new WaitForSeconds(28);
        animator.SetTrigger("Idle");
        isDigesting = false; // 结束消化，回到 Idle
    }

    // 找最近僵尸
    Zombie FindClosestZombie()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Zombie closest = null;
        float minDistX = Mathf.Infinity;

        foreach (Zombie z in zombies)
        {
            if (z.GetComponent<Collider2D>().enabled == false) continue;
            float distX = z.transform.position.x - transform.position.x;
            float distY = Mathf.Abs(z.transform.position.y - transform.position.y);
            if (distX < minDistX && distX <= attackRange&&distX>=0&&distY<= yattckRange)
            {
                minDistX = distX;
                closest = z;
            }
        }
        return closest;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 pos = transform.position + Vector3.right * attackRange * 0.5f;
        Gizmos.DrawWireCube(pos, new Vector3(attackRange, yattckRange, 1));
    }
}