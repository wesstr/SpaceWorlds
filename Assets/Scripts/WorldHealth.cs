using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHealth : MonoBehaviour {

    public float m_WorldHealth;
    public GameObject m_ExplosionPrefab;
    public AudioClip m_WorldLaserHit;

    private bool m_IsDestroyed;
    private AudioSource m_AudioSource;
    private int m_SoundsAtOnce = 0;
	

	void Start () {
        m_AudioSource = GetComponent<AudioSource>();
        m_WorldHealth = 10;
        m_IsDestroyed = false;
	}
	
	
	void Update () {
		if (m_WorldHealth <= 0 && !m_IsDestroyed)
        {
            Destroy(this.gameObject);
            GameManager.m_Score += 1;
            m_IsDestroyed = true;
        }
	}

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Destroy(Instantiate(m_ExplosionPrefab, other.transform.position, other.transform.rotation), 1f);
            m_AudioSource.clip = m_WorldLaserHit;
            if (!m_AudioSource.isPlaying && (m_SoundsAtOnce <= 3))
            {
                m_SoundsAtOnce++;
                m_AudioSource.Play();
                Destroy(other.gameObject);
            }
            else
            {
                m_SoundsAtOnce = 0;
            }
            m_WorldHealth--;
        } 
    }
}
