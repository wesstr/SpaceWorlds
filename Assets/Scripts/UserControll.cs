using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControll : MonoBehaviour {

    public float fireRate = 0;
    public float m_moveSpeed;
    public Camera m_Camera;
    public float m_SpawnDistance;
    public int rotationOffset;
    public GameObject m_ExplosionPrefab;
    public int m_DamageFromWorlds;
    public AudioClip m_ShootSound;
    public AudioClip m_DamageSound;


    private float timeToFire = 0;
    private Rigidbody m_Rigidbody;
    private Vector3 m_CorrectedDistnace;
    private AudioSource m_AudioListner;


	void Start () {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_AudioListner = this.GetComponent<AudioSource>();
        m_CorrectedDistnace = m_Camera.ViewportToWorldPoint(new Vector3(0, 1, m_Camera.nearClipPlane + m_SpawnDistance));
        m_CorrectedDistnace = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, m_CorrectedDistnace.z);
        gameObject.transform.position = m_CorrectedDistnace;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (Time.time > timeToFire))
        {
            timeToFire = Time.time + 1 / fireRate;
            m_AudioListner.clip = m_ShootSound;
            m_AudioListner.Play();
        }
    }

    void FixedUpdate () {
        m_Rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * m_moveSpeed, Input.GetAxis("Vertical") * m_moveSpeed, 0f);
        Vector3 difference = m_Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.nearClipPlane + m_SpawnDistance)) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, rotZ + rotationOffset);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (collision.gameObject.tag == "World")
        {
            GameManager.m_Health -= m_DamageFromWorlds;
            m_AudioListner.clip = m_DamageSound;
            m_AudioListner.Play();
            Destroy(Instantiate(m_ExplosionPrefab, pos, rot), 1f);
        }
    }
}
