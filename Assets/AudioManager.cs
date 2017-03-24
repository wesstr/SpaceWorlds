using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public AudioClip[] m_AudioClips = new AudioClip[4];

    public AudioSource m_AudioSource;
    // Use this for initialization
    void Start()
    {
        //m_AudioSource = this.GetComponent<AudioSource>();
        m_AudioSource.volume = 0;
        Shader.WarmupAllShaders();
        RandomSound(m_AudioClips);

    }

    // Update is called once per frame
    void Update()
    {
        lerpVolumeChange();
        if (!m_AudioSource.isPlaying)
        {
            m_AudioSource.volume = 0.2f;
            RandomSound(m_AudioClips);
        }
    }
    public void lerpVolumeChange()
    {
        if (m_AudioSource.volume < 0.6 && (m_AudioSource.time < (m_AudioSource.clip.length * .85f)))
            m_AudioSource.volume += 0.1f * Time.deltaTime;
        if (m_AudioSource.time > (m_AudioSource.clip.length * .85f))
            m_AudioSource.volume -= 0.1f * Time.deltaTime;
    }
    public void RandomSound(params AudioClip[] clips)
    {
        int r = Random.Range(0, clips.Length);
        m_AudioSource.clip = clips[r];
        m_AudioSource.Play();
    }
}
