using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 1;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    public float m_MoveSpeed;
    public float m_DestoryAfter;

    private float timeToFire = 0;
    private Transform firePoint;
    private Rigidbody m_LaserRigibdoy;


    void Awake()
    {
        firePoint = this.transform;
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint");
        }
    }


    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }


    void Shoot()
    {
        m_LaserRigibdoy = Instantiate(BulletTrailPrefab, transform.position , transform.rotation).GetComponent<Rigidbody>();
        m_LaserRigibdoy.velocity = transform.forward * m_MoveSpeed;
        Destroy(m_LaserRigibdoy.gameObject, m_DestoryAfter);
    }
}
