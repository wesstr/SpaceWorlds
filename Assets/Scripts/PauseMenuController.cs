using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuController : MonoBehaviour {

    public GameObject m_GameManagerGameObject;

    private bool m_PauseMenuOpen;
    private Animator m_PauseMenuAnimator;


	void Start () {
        m_PauseMenuAnimator = GetComponent<Animator>();
        m_PauseMenuOpen = false;
        m_PauseMenuAnimator.SetBool("IsOpen", false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !m_PauseMenuOpen)
        {
            m_PauseMenuOpen = true;
            m_PauseMenuAnimator.SetBool("IsOpen", m_PauseMenuOpen);
            StartCoroutine(WaitToTimeScale());
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && m_PauseMenuOpen)
        {
            Resume();
        }
	}

    IEnumerator WaitToTimeScale()
    {
        yield return new WaitForSeconds(0f);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        m_PauseMenuOpen = false;
        m_PauseMenuAnimator.SetBool("IsOpen", m_PauseMenuOpen);
        Time.timeScale = 1;
    }

    public void Quit(string level)
    {
        StartCoroutine(StartGame(level));
        Time.timeScale = 1;
    }

    IEnumerator StartGame(string level)
    {
        float fadeTime = m_GameManagerGameObject.GetComponent<Fading>().BeginFade(3);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }
}
