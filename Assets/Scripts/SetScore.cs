using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour {

    private Text m_ScoreText;

	void Start () {
        m_ScoreText = this.gameObject.GetComponent<Text>();
	}
	
	void Update () {
        m_ScoreText.text = " Score: " + GameManager.m_Score;
	}
}
