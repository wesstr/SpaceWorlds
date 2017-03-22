using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour {

    public float m_MoveSpeed;
    public float m_DestoryAfter;

    private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start () {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.up * m_MoveSpeed;
        Destroy(this.gameObject, m_DestoryAfter);

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        //m_Rigidbody.velocity = transform.forward * m_MoveSpeed;
	}
}
