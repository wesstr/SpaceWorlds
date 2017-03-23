using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreData : MonoBehaviour {

    public GameObject m_NamePrefab;
    public GameObject m_ScorePrefab;
    public GameObject m_NameContainer;
    public GameObject m_ScoreContainer;
    public GameManager m_GameManager;

    private Text m_NameText;
    private Text m_ScoreText;
    private GameObject m_JustMade;
    private List<string> m_Names;
    private List<int> m_Scors;

	void Start () {
        m_Names = m_GameManager.GetNames();
        m_Scors = m_GameManager.GetScors();

        Debug.Log(m_Names.Count);

        for (int i = 0; m_Scors.Count != i; i++)
        {
            m_JustMade = Instantiate(m_NamePrefab);
            m_JustMade.GetComponent<Text>().text = m_Names[i] + " >";
            m_JustMade.transform.parent = m_NameContainer.transform;
            m_JustMade = Instantiate(m_ScorePrefab);
            m_JustMade.GetComponent<Text>().text = "Score: " + m_Scors[i];
            m_JustMade.transform.parent = m_ScoreContainer.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
