using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject m_EndGameMenu;
    public MainMenuController m_MainMenuController;
    public GameObject m_PlayerGameObject;

    public static int m_Score;
    public static int m_Health;
    public static string m_PlayerName;
    public int m_LastScore;

    private ScoreData m_ScoreData;

	void Awake () {
        m_Score = 0;
        m_Health = 100;
        m_ScoreData = LoadUserData();
        if (m_ScoreData == null)
            Debug.Log("Failed to load score data!");
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Health <= 0)
        {
            Destroy(m_PlayerGameObject);
            m_EndGameMenu.SetActive(true);
            //Time.timeScale = 0;
        }
	}

    public List<String> GetNames()
    {
        return m_ScoreData.names;
    }

    public List<int> GetScors()
    {
        return m_ScoreData.scores;
    }

    public void Save(string name)
    {
        m_PlayerName = name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Append);
        Debug.Log(Application.persistentDataPath);
        //ScoreData data = new ScoreData();
        m_ScoreData.names.Insert(0, m_PlayerName);
        m_ScoreData.scores.Insert(0, m_Score);
        Debug.Log(m_ScoreData.scores.Count);

        bf.Serialize(file, m_ScoreData);
        file.Close();
    }

    ScoreData LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerScores.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Open);
            ScoreData data = (ScoreData) bf.Deserialize(file);
            file.Close();

            //m_LastScore = data.scores[0];
            //m_PlayerName = data.names[0];
            return data;
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
class ScoreData
{
    public List<string> names = new List<string>();
    public List<int> scores = new List<int>();
}