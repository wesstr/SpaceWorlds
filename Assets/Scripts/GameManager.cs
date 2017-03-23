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

	void Awake () {
        m_Score = 0;
        m_Health = 100;
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

    public void Save(string name)
    {
        m_PlayerName = name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Append);

        ScoreData data = new ScoreData();
        data.names.Insert(0, m_PlayerName);
        data.scores.Insert(0, m_Score);

        bf.Serialize(file, data);
        file.Close();
    }

    void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerScores.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Open);
            ScoreData data = (ScoreData)bf.Deserialize(file);
            file.Close();

            m_LastScore = data.scores[0];
            m_PlayerName = data.names[0];
        }
    }
}

[Serializable]
class ScoreData
{
    public List<string> names = new List<string>();
    public List<int> scores = new List<int>();
}