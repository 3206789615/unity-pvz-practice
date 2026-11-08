using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeheadZombie : Zombie
{
    public int Conehead = 250;
    private int Hp = 250;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Takedamage(int damage)
    {
        if (Conehead>0)
        {
            isLostHP = false;
            AudioManager.Instance.Play("plastichit");
            this.Conehead -= damage;
        } 
        
        if (Conehead <= 0)
        {
            Conehead = -1;
            isLostHP = true;
        }
        float protectLife = Conehead * 1f / Hp;
        animator.SetFloat("ProtectLife", protectLife);

        base.Takedamage(damage);
    }

   
}