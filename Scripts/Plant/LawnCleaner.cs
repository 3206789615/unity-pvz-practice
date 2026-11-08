using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnCleaner : MonoBehaviour
{
    public float speed;
    private bool ismove = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            ismove = true;
            AudioManager.Instance.Play("lawnmower");
            collision.GetComponent<Zombie>().Takedamage(9999);
            Destroy(gameObject, 8f);
        }
    }

    void Update()
    {
        if (ismove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
    }
}
