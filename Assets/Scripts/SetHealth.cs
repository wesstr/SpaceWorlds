using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealth : MonoBehaviour {

    private Slider m_HealthSlider;

	void Start () {
        m_HealthSlider = GetComponent<Slider>();
        m_HealthSlider.maxValue = GameManager.m_Health;
	}
	
	// Update is called once per frame
	void Update () {
        m_HealthSlider.value = GameManager.m_Health;
	}
}
