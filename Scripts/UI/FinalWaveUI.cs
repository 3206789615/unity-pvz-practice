using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWaveUI : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    private void Start()
    {
        Hide();
    }

    void Hide()
    {
        anim.enabled = false;
    }

    public void FWShow()
    {
        anim.enabled = true;
    }
}
