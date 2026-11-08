using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 3;
    private int atkValue = 30;
    public GameObject peaBulletHitPrefab;
    private bool hashit = false;
    public void SetATKValue(int atkValue)
    {
       this.atkValue = atkValue;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hashit) return;
        if (!collision.CompareTag("Zombie")) return;

        Zombie zombie = collision.GetComponent<Zombie>();

        if (zombie == null) return; // ?关键1：防销毁对象

        hashit = true;

        zombie.Takedamage(atkValue); // ?先打

        Destroy(gameObject); // ?再销毁自己（顺序也优化）

        GameObject go = Instantiate(peaBulletHitPrefab, transform.position, Quaternion.identity);
        Destroy(go, 1);
    }
}
