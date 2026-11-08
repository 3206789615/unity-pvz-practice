using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Squash : Plant
{
    private Animator anim;
    private Transform target;

    public LayerMask zombieLayer;
    Vector2 boxSize = new Vector2(3.6f, 0.8f);  // 前后两格
    public float jumpHeight = 2f;       // 跳跃高度
    public float jumpTime = 0.4f;       // 跳跃时间
                                        // 压死范围
    Vector2 BoxSize = new Vector2(1.5f, 0.8f);
    private bool hasAttacked = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void EnableUpdate()
    {
        if (hasAttacked) return;

        DetectZombie();
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
            StartCoroutine(JumpAttack());
        }
    }

    IEnumerator JumpAttack()
    {
        AudioManager.Instance.Play("squash_hmm");
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        //anim.SetTrigger("attack");

        Vector3 start = transform.position;
        Vector3 end = target.position;

        float time = 0;

        while (time < jumpTime)
        {
            time += Time.deltaTime;
            float t = time / jumpTime;

            // 抛物线
            float height = jumpHeight * 4 * t * (1 - t);

            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y += height;

            transform.position = pos;

            yield return null;
        }

        // 到达位置
        transform.position = end;

        // 压死范围内所有僵尸
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            BoxSize,
            0,
            zombieLayer
        );        
        foreach (var hit in hits)
        {
            hit.GetComponent<Zombie>().Takedamage(900);
        }       

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // 检测范围（找目标用）
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);

        // 攻击范围（压死用）
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }
}










