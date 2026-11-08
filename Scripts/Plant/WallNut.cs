using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    private int currentHP;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        currentHP = HP;
    }
    
    protected override void EnableUpdate()
    {
        Damage();
    }

    private void Damage()
    {
        float hpPrecent = HP * 1f / currentHP;
        anim.SetFloat("HPPrecent", hpPrecent);      
    }
}
