using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PlantState
{
    Disable,
    Enable,
}
public class Plant : MonoBehaviour
{
    PlantState plantState = PlantState.Disable;
    public PlantType plantType = PlantType.Sunflower;

    public int HP = 100;    
    private void Start()
    {
        TransitionToDisable();
    }
    private void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate();
                break;
            case PlantState.Enable:
                EnableUpdate();
                break;
            default:
                break;
        }
    }

    protected virtual void DisableUpdate()
    {

    }
    protected virtual void EnableUpdate()
    {

    }

    public void TransitionToDisable()
    {
        plantState = PlantState.Disable;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void TransitionToEnable()
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP<=0)
        {
            Die();
        }    
    }

    public void Die()
    {
        PlantManager.Instance.RemovePlant(this);
        TransitionToDisable();
        Destroy(gameObject);        
    }
}
