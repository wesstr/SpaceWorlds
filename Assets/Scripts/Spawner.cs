using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] m_plantes;
    public float m_moveSpeed;
    public float m_rotationSpeed;
    public Camera m_Camera;
    public float m_SpawnDistance;
    public float m_timeToDestory;
    public float m_toSpawn;
    public float m_spawnDelay;
    public float m_distanceBetweenWorlds;
    public float m_WorldLifeTime;
    public AnimationCurve m_SpawnRate;
    
    private Rigidbody m_rigidbody;
    private int m_randomNum;
    private GameObject m_justSpawned;
    private int m_spawned;
    private Vector3 m_topLeft;
    private Vector3 m_topRight;
    private Vector3 m_bottomLeft;
    private Vector3 m_bottomRight;
    private Vector3 m_LeftCenterPoint;
    private Vector3 m_RightCenterPoint;
    private Vector3 m_TopCenterPoint;
    private Vector3 m_BottomCenterPoint;
    private Keyframe[] m_AnimationCurvePoints;
    private int m_WavesSpawned = 0;
    private bool m_IsSpawning;

	// Use this for initialization
	void Awake () {
        m_topLeft = m_Camera.ViewportToWorldPoint(new Vector3(0, 1 , m_Camera.nearClipPlane + m_SpawnDistance));
        m_topRight = m_Camera.ViewportToWorldPoint(new Vector3(1, 1, m_Camera.nearClipPlane + m_SpawnDistance));
        m_bottomLeft = m_Camera.ViewportToWorldPoint(new Vector3(0, 0, m_Camera.nearClipPlane + m_SpawnDistance));
        m_bottomRight = m_Camera.ViewportToWorldPoint(new Vector3(1, 0, m_Camera.nearClipPlane + m_SpawnDistance));
        m_AnimationCurvePoints = m_SpawnRate.keys;
        m_LeftCenterPoint = Vector3.Lerp(m_topLeft, m_bottomLeft, 0.5f);
        m_RightCenterPoint = Vector3.Lerp(m_topRight, m_bottomRight, 0.5f);
        m_TopCenterPoint = Vector3.Lerp(m_topLeft, m_topRight, 0.5f);
        m_BottomCenterPoint = Vector3.Lerp(m_bottomLeft, m_bottomRight, 0.5f);
    }

    private void Update()
    {
        if ((m_WavesSpawned != m_AnimationCurvePoints.Length))
        {
            if (((int)Time.timeSinceLevelLoad <= (int)m_AnimationCurvePoints[m_WavesSpawned].time) && !m_IsSpawning)
            {
                m_IsSpawning = true;
                for (int i = 0; i <= (int)m_AnimationCurvePoints[m_WavesSpawned].value; i++)
                {
                    StartCoroutine(SpawnDelayLeft(m_spawnDelay));
                    StartCoroutine(SpawnDelayRight(m_spawnDelay));
                    StartCoroutine(SpawnDelayTop(m_spawnDelay));
                    StartCoroutine(SpawnDelayBottom(m_spawnDelay));
                }
                m_WavesSpawned++;
            }
            else if ((int)Time.timeSinceLevelLoad >= (int)m_AnimationCurvePoints[m_WavesSpawned].time)
            {
                m_IsSpawning = false;
            }
        }
    }

    private Vector3 WhereToSpawnLeft()
    {
        float randomPoint;
        randomPoint = Random.Range(m_bottomLeft.y, m_topLeft.y);
        return new Vector3(m_topLeft.x + m_bottomLeft.x, randomPoint + m_distanceBetweenWorlds, m_SpawnDistance);
    }

    private Vector3 WhereToSpawnRight()
    {
        float randomPoint;
        Vector3 spawnPoint;
        randomPoint = Random.Range(m_bottomRight.y, m_topRight.y);
        spawnPoint = new Vector3(m_bottomRight.x + m_topRight.x, randomPoint + m_distanceBetweenWorlds, m_SpawnDistance);
        return spawnPoint;
    }

    private Vector3 WhereToSpawnBottom()
    {
        float randomPoint;
        Vector3 spawnPoint;
        randomPoint = Random.Range(m_bottomLeft.x, m_bottomRight.x);
        spawnPoint = new Vector3(randomPoint + m_distanceBetweenWorlds, m_bottomRight.y + m_bottomLeft.y, m_SpawnDistance);
        return spawnPoint;
    }

    private Vector3 WhereToSpawnTop()
    {
        float randomPoint;
        Vector3 spawnPoint;
        randomPoint = Random.Range(m_topLeft.x, m_topRight.x);
        spawnPoint = new Vector3(randomPoint + m_distanceBetweenWorlds, m_topRight.y + m_topLeft.y , m_SpawnDistance);
        return spawnPoint;
    }

    IEnumerator SpawnDelayRight(float waitTime)
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(-m_moveSpeed, -Random.Range(m_BottomCenterPoint.y, m_TopCenterPoint.y), 0f);
        Rigidbody rgbdy;
        yield return new WaitForSeconds(Random.Range(0f, waitTime));
        randomnum = Random.Range(0, m_plantes.Length);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnRight(), Quaternion.identity);
        Destroy(justSpawned.gameObject, m_WorldLifeTime);
        rgbdy = justSpawned.GetComponent<Rigidbody>();
        rgbdy.velocity = move;
        move.x += m_rotationSpeed;
        rgbdy.AddRelativeTorque(move);
    }

    IEnumerator SpawnDelayLeft(float waitTime)
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(m_moveSpeed, Random.Range(m_BottomCenterPoint.y, m_TopCenterPoint.y), 0f);
        Rigidbody rgbdy;
        yield return new WaitForSeconds(Random.Range(0f, waitTime));
        randomnum = Random.Range(0, m_plantes.Length);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnLeft(), Quaternion.identity);
        Destroy(justSpawned.gameObject, m_WorldLifeTime);
        rgbdy = justSpawned.GetComponent<Rigidbody>();
        rgbdy.velocity = move;
        move.x += m_rotationSpeed;
        rgbdy.AddRelativeTorque(move);
    }

    IEnumerator SpawnDelayBottom(float waitTime)
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(Random.Range(m_LeftCenterPoint.x, m_RightCenterPoint.x), m_moveSpeed, 0f);
        Rigidbody rgbdy;
        yield return new WaitForSeconds(Random.Range(0f, waitTime));
        randomnum = Random.Range(0, m_plantes.Length);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnBottom(), Quaternion.identity);
        Destroy(justSpawned.gameObject, m_WorldLifeTime);
        rgbdy = justSpawned.GetComponent<Rigidbody>();
        rgbdy.velocity = move;
        move.x += m_rotationSpeed;
        rgbdy.AddRelativeTorque(move);
    }

    IEnumerator SpawnDelayTop(float waitTime)
    {
        GameObject justSpawned;
        int randomnum;
        Vector3 move = new Vector3(-Random.Range(m_LeftCenterPoint.x, m_RightCenterPoint.x), -m_moveSpeed, 0f);
        Rigidbody rgbdy;
        yield return new WaitForSeconds(Random.Range(0f, waitTime));
        randomnum = Random.Range(0, m_plantes.Length);
        justSpawned = Instantiate(m_plantes[randomnum], WhereToSpawnTop(), Quaternion.identity);
        Destroy(justSpawned.gameObject, m_WorldLifeTime);
        rgbdy = justSpawned.GetComponent<Rigidbody>();
        rgbdy.velocity = move;
        move.x += m_rotationSpeed;
        rgbdy.AddRelativeTorque(move);
    }
}