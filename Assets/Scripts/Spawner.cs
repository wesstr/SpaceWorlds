using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] m_plantes;
    int m_randomNum;
    public Vector3 m_move;
    private Rigidbody m_rigidbody;
    GameObject m_justSpawned;

	// Use this for initialization
	void Start () {
        m_randomNum = Random.Range(0, 1);
        m_justSpawned = Instantiate(m_plantes[m_randomNum]);
        m_rigidbody = m_justSpawned.GetComponent<Rigidbody>();
        m_rigidbody.AddRelativeForce(m_move);
        m_rigidbody.AddRelativeTorque(m_move);
    }

    private void FixedUpdate()
    {

    }
}
