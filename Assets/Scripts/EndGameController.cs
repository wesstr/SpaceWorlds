using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour {

    private InputField m_NameInput;
    private GameManager m_GameManager;

	
	void Start () {
        m_NameInput = GetComponentInChildren<InputField>();
        m_GameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
        this.gameObject.SetActive(false);
	}

    public void SaveScore()
    {
        m_GameManager.Save(m_NameInput.text);
    }

    public void Restart(string name)
    {
        StartCoroutine(StartGame(name));
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    IEnumerator StartGame(string level)
    {
        float fadeTime = m_GameManager.GetComponent<Fading>().BeginFade(3);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }
}
