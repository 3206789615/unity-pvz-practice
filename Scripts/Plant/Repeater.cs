using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Repeater : Plant
{
    public float shootDuration;
    private float shootTimer = 0;    
    public Transform shootPointTransform;
    public PeaBullet peaBulletPrefab;

    public float bulletSpeed = 5;
    public int atkValue = 30;


    public LayerMask mask;
    public float length = 100;


    //private void Update()
    //{
    //Ray2D ray = new Ray2D(transform.position, Vector2.right);
    // RaycastHit2D hitInfo= Physics2D.Raycast(ray.origin, ray.direction, length, mask);

    // }
    protected override void EnableUpdate()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.right);
        RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction, length, mask);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Zombie")
        {
            shootTimer += Time.deltaTime;           
            if (shootTimer > shootDuration)
            {
                AudioManager.Instance.Play("shoot");
                AudioManager.Instance.Play("shoot");
                shoot();
                Invoke("shoot", 0.17f);
                shootTimer = 0;
            }            
        }

        //Debug.DrawRay(transform.position, Vector2.right*10,Color.blue);
    }

    void shoot()
    {
        PeaBullet pb = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);
        pb.SetSpeed(bulletSpeed);
        pb.SetATKValue(atkValue);
    }
}
