using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketheadZombie : Zombie
{
    public int Buckethead = 600;
    private int Hp = 600;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Takedamage(int damage)
    {
        if (Buckethead > 0)
        {
            AudioManager.Instance.PlayRandom("shieldhit",2);
            isLostHP = false;
            this.Buckethead -= damage;
        }

        if (Buckethead <= 0)
        {
            Buckethead = -1;
            isLostHP = true;
        }
        float protectLife = Buckethead * 1f / Hp;
        animator.SetFloat("ProtectLife", protectLife);

        base.Takedamage(damage);
    }
}
