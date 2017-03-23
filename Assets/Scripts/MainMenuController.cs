using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }

    public void Start(string level)
    {
        StartCoroutine(StartGame(level));
    }

    IEnumerator StartGame(string level)
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(3);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }
}