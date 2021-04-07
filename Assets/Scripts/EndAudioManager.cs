using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAudioManager : MonoBehaviour
{
    public GameObject creds;
    public GameObject einstein;
    public AudioSource bgm;
    public AudioSource eins;
    public Material em;
    void Start()
    {
        Invoke("Einstein", 7f);
        Invoke("Credits", 24f);
    }

    public void Einstein()
    {
        eins.Play();
        einstein.GetComponent<Animator>().Play("Nod");
        em.EnableKeyword("_EMISSION");
    }

    public void Credits()
    {
        bgm.Play();
        creds.SetActive(true);
    }
    private void Update()
    {
        if(Time.timeSinceLevelLoad>25)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}
