using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 1;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    //public AudioClip slimeFire;

    private float timeToFire = 0;
    Transform firePoint;
    // Use this for initialization
    void Awake()
    {
        firePoint = this.transform;
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(BulletTrailPrefab, transform.position , Quaternion.identity);
    }

    void Effect()
    {
        //BulletTrailPrefab.gameObject.GetComponent<MoveTrail>().damage = damage;
        Instantiate(BulletTrailPrefab, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, 90 + firePoint.eulerAngles.z));
    }
}
