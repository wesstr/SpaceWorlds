using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControll : MonoBehaviour {

    public float m_moveSpeed;
    public Camera m_Camera;
    public float m_SpawnDistance;

    private Rigidbody m_Rigidbody;
    private Vector3 m_CorrectedDistnace;

	// Use this for initialization
	void Start () {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_CorrectedDistnace = m_Camera.ViewportToWorldPoint(new Vector3(0, 1, m_Camera.nearClipPlane + m_SpawnDistance));
        m_CorrectedDistnace = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, m_CorrectedDistnace.z);
        gameObject.transform.position = m_CorrectedDistnace;
    }

    // Update is called once per frame
    void FixedUpdate () {
        m_Rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * m_moveSpeed, Input.GetAxis("Vertical") * m_moveSpeed, 0f);      
    }
}
