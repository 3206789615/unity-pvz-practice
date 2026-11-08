using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum ZombieState
{
    Move,
    Eat,
    Die,
    Pause
}
public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rgd;
    public float moveSpeed = 2;
    private Animator anim;

    public int atkValue=30;
    public float atkDuration = 2;
    private float atkTimer = 0;

    private Plant currentEatPlant;

    private int HP = 100;
    public int currentHP;
    public GameObject zombieHeadPrefab;
    private bool haveHead = true;

    private bool eatLost = true;
    private bool isSlow = false;
    public bool isLostHP = true;


    public float lost = 2;
    private float lostTimer = 0;

    private float deathTimer = 0f;
    public float deathDuration = 2f;

    private Vector3 vector3 = new Vector3(-7, 0, 0);    
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (Random.value < 0.2f)
        {
            AudioManager.Instance.PlayRandom("groan", 6);
        }
        currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:                
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                deathTimer += Time.deltaTime;
                if (deathTimer >= deathDuration)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }

        lostTimer += Time.deltaTime;
        if (lostTimer >lost &&!haveHead&&currentHP>0)
        {
            currentHP -= 10;
            lostTimer = 0;
            if (currentHP <= 0)
            {
                Dead();
            }
            float hpPrecent = currentHP * 1f / HP;
            anim.SetFloat("HPPrecent", hpPrecent);
        }
    }

    void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime);
    }
    void EatUpdate()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer>atkDuration)
        {           
            if (currentEatPlant == null)
            {
                anim.SetBool("IsEating", false);
                zombieState = ZombieState.Move;
                currentEatPlant = null;
                return;
            }
            AudioManager.Instance.PlayRandom("chomp",2);
            currentEatPlant.TakeDamage(atkValue);
            atkTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Plant")
        {
            anim.SetBool("IsEating", true);
            TransitionToEat();
            currentEatPlant = collision.GetComponent<Plant>();
            atkTimer = 0;
        }
        if (collision.tag=="House")
        {
            transform.DOMove(vector3, 1f);
            GameManager.Instance.GameFalseEnd();
        }
    
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant"&&eatLost)
        {
            if (collision.GetComponent<Plant>() == currentEatPlant)
            {
                anim.SetBool("IsEating", false);
                zombieState = ZombieState.Move;
                currentEatPlant = null;
            }
        }
    }

    void TransitionToEat()
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }
    public void TransitionToPause()
    {
        zombieState = ZombieState.Pause;
       //GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        anim.enabled = false;
    }

    public virtual void Takedamage(int damage)
    {
        if (!isLostHP) return;
        if (currentHP <= 0) return;
        this.currentHP -= damage;
        if (currentHP <= 0)
        {          
            Dead();
        }
        float hpPrecent = currentHP * 1f / HP;
        anim.SetFloat("HPPrecent", hpPrecent);
        if (hpPrecent<0.5f&&haveHead)
        {
           haveHead = false;
           GameObject go= GameObject.Instantiate(zombieHeadPrefab, transform.position, Quaternion.identity);
            Destroy(go, 2);
        }
        
    }
    public void Dead()
    {
        if (zombieState == ZombieState.Die) return;
        currentHP = -1;
        eatLost = false;      
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        ZombieManager.Instance.RemoveZombie(this);
        deathTimer = 0f;
    }
    private bool isRemoved = false;

    private void OnDestroy()
    {
        if (ZombieManager.Instance != null && !isRemoved)
        {
            ZombieManager.Instance.RemoveZombie(this);
            isRemoved = true;
        }
    }

    public void TurnBlue()
    {
        if (isSlow==false)
        {
            moveSpeed /= 2;
            atkDuration *= 2;
            anim.speed /= 2;
            isSlow = true;           
        }
        CancelInvoke(nameof(Recover));
        GetComponent<SpriteRenderer>().color = Color.blue;
        Invoke(nameof(Recover), 5f);

    }

    private void Recover()
    {
        moveSpeed *= 2;
        atkDuration /= 2;
        anim.speed *= 2;
        isSlow = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
