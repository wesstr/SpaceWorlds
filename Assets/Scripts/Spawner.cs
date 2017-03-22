using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] m_plantes;
    public Vector3 m_move;
    public Camera m_Camera;
    public float m_SpawnDistance;
    public float m_timeToDestory;
    public int m_toSpawn;
    public int m_spawnDelay;
    

    private Rigidbody m_rigidbody;
    private int m_randomNum;
    private GameObject m_justSpawned;
    private int m_spawned;
    private Vector3 m_topLeft;
    private Vector3 m_topRight;
    private Vector3 m_bottomLeft;
    private Vector3 m_bottomRight;

	// Use this for initialization
	void Awake () {
        m_topLeft = m_Camera.ViewportToWorldPoint(new Vector3(0, 1 , m_Camera.nearClipPlane + m_SpawnDistance));
        m_topRight = m_Camera.ViewportToWorldPoint(new Vector3(1, 1, m_Camera.nearClipPlane + m_SpawnDistance));
        m_bottomLeft = m_Camera.ViewportToWorldPoint(new Vector3(0, 0, m_Camera.nearClipPlane + m_SpawnDistance));
        m_bottomRight = m_Camera.ViewportToWorldPoint(new Vector3(1, 0, m_Camera.nearClipPlane + m_SpawnDistance));
        //Debug.Log(m_topLeft);
    }

    private void FixedUpdate()
    {
        if (m_toSpawn != m_spawned)
        {
            StartCoroutine(SpawnDelayLeft());
            StartCoroutine(SpawnDelayRight());
            m_spawned++;
        }
    }

    private Vector3 WhereToSpawnLeft()
    {
        float randomPoint;
        randomPoint = Random.Range(m_bottomLeft.y, m_topLeft.y);
        return new Vector3(m_topLeft.x + m_bottomLeft.x, randomPoint, m_bottomLeft.z);
    }

    private Vector3 WhereToSpawnRight()
    {
        float randomPoint;
        Vector3 spawnPoint;
        randomPoint = Random.Range(m_bottomRight.y, m_topRight.y);
        spawnPoint = new Vector3(m_bottomRight.x + m_topRight.x, randomPoint, m_SpawnDistance);
        Debug.Log(spawnPoint);
        return spawnPoint;
    }

    IEnumerator SpawnDelayRight()
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(-50f, 0f, 0f);
        yield return new WaitForSeconds(Random.Range(0f, 10f));
        Debug.Log("Spawn Right");
        randomnum = Random.Range(0, 1);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnRight(), new Quaternion());
        m_rigidbody = justSpawned.GetComponent<Rigidbody>();
        m_rigidbody.AddRelativeForce(move);
        m_rigidbody.AddRelativeTorque(move);
    }

    IEnumerator SpawnDelayLeft()
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(50f, 0f, 0f);
        yield return new WaitForSeconds(Random.Range(0f,10f));
        Debug.Log("Spawn Left");
        Debug.Log(WhereToSpawnLeft());
        randomnum = Random.Range(0, 1);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnLeft(), new Quaternion());
        m_rigidbody = justSpawned.GetComponent<Rigidbody>();
        m_rigidbody.AddRelativeForce(move);
        m_rigidbody.AddRelativeTorque(move);
    }

    void OnDrawGizmosSelected()
    {
       
        Vector3 p = m_Camera.ViewportToWorldPoint(new Vector3(1, 1, m_Camera.nearClipPlane + 50));
        Debug.Log(p);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.5F);
    }
}
