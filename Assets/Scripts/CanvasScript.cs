using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject ftl;
    public GameObject parameters;
    public void PauseMenu()
    {
        panel.SetActive(true);
        parameters.SetActive(false);
        ftl.GetComponent<FTLMeter>().enabled = false;
        ftl.GetComponent<EndlessRunnerController>().enabled = false;
        Time.timeScale = .0001f;
    }
    public void Resume()
    {
        panel.SetActive(false);
        parameters.SetActive(false);
        ftl.GetComponent<FTLMeter>().enabled = true;
        ftl.GetComponent<EndlessRunnerController>().enabled = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void DebugMenu()
    {
        parameters.SetActive(true);
        panel.SetActive(false);
    }
    
}
