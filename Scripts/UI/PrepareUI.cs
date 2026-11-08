using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Show()
    {
        anim.enabled = true;
    }

    void OnShowComplete()
    {

    }
}
