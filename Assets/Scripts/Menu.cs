using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource BG;
    float time = 0;
    public GameObject panel;
    public GameObject TutButton;
    void Start()
    {
        BG.time = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.unscaledDeltaTime;
        if(time>4.5f)
        {
            panel.SetActive(true);
            panel.GetComponent<Animator>().speed = 1 / Time.timeScale;
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("Tutorialdone") != 1)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("Game");
    }
    
    public void LoadTut()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
