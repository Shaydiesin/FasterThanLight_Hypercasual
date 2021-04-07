using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    GameObject player;
    SwipeManager swipe;
    public int state = 0;
    bool inTransition;
    //bool endOfTut;
    public float transitionTime = 1.0f;
    public float loadSceneTime = 4.0f;
    public GameObject Panel;
    List<GameObject> States;
    public List<string> AnimationNames;
    public WeaponSpawn weaponSpawnner;
    public WeaponSpawnTutorial weaponSpawnnerTut;
    public EnemySpawn enemySpawn;
    public int numEnemyToKill = 4;   
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        inTransition = false;
        //endOfTut = false;
        state = 0;
        player = GameObject.Find("player");
        States = new List<GameObject>();
        for (int i=0; i< Panel.transform.childCount; i++)
        { 
            States.Add(Panel.transform.GetChild(i).gameObject);
        }
        for (int i=0; i< Panel.transform.childCount; i++)
        { 
            States[i].SetActive(false);
        }
        States[0].SetActive(true);

        AnimationNames = new List<string>();
        AnimationNames.Add("SwipLeftTutEnter");
        AnimationNames.Add("SwipeRightTutEnter");
        AnimationNames.Add("HoldTutEnter");
        AnimationNames.Add("WeaponTutEnter");
        AnimationNames.Add("KillTutEnter");
        AnimationNames.Add("DodgeTutEnter");
        AnimationNames.Add("CompleteTutEnter");
        anim = Panel.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        anim.SetFloat("invTimeScale", 1/Time.timeScale);
        if (!inTransition)
        {
            if (state == 0)
            {
                SwipeManager.swipeRight = false;
                SwipeManager.swipeUp = false;
                SwipeManager.swipeDown = false;
                SwipeManager.tap = false;
                if (SwipeManager.swipeLeft == true)
                {
                    // go to swipe right
                    inTransition = true;
                    StartCoroutine(ChangeTransitionState());
                }
            }
            else if (state == 1)
            {
                SwipeManager.swipeUp = false;
                SwipeManager.swipeLeft = false;
                SwipeManager.swipeUp = false;
                SwipeManager.swipeDown = false;
                if (SwipeManager.swipeRight == true)
                {
                    // go to hold
                    inTransition = true;
                    StartCoroutine(ChangeTransitionState());
                }
            }
            else if (state == 2)
            {
                if (SwipeManager.tap == true)
                {
                    // go to pick
                    inTransition = true;
                    StartCoroutine(ChangeTransitionState());
                    weaponSpawnner.transform.Translate(Vector3.forward * (player.transform.position.z));
                }
            }
            else if (state == 3)
            {
                weaponSpawnner.gameObject.SetActive(true);
                weaponSpawnner.enabled = false;
                weaponSpawnnerTut.enabled = true;
                if (player.GetComponent<SuperHotScript>().weapon != null)
                {
                    // go to kill
                    inTransition = true;
                    StartCoroutine(ChangeTransitionState());
                }
            }
            else if (state == 4)
            {
                weaponSpawnner.gameObject.SetActive(true);
                weaponSpawnnerTut.enabled = false;
                weaponSpawnner.enabled = true;
                enemySpawn.transform.Translate(Vector3.forward * (player.transform.position.z));
                enemySpawn.gameObject.SetActive(true);
                foreach (GameObject enemy in enemySpawn.Enemies)
                {
                    enemy.GetComponent<Animator>().SetBool("transition", false);
                }
                if (player.GetComponent<SuperHotScript>().weapon == null)
                {
                    // go to dodge
                    inTransition = true;
                    StartCoroutine(ChangeTransitionState());
                }
            }
            else if (state == 5)
            {
                // wait for player to kill 3 then go to end screen
                if (player.GetComponent<SuperHotScript>().enemiesKilled >= numEnemyToKill)
                {
                    States[state].SetActive(false);
                    States[state + 1].SetActive(true);
                    anim.Play(AnimationNames[state + 1]);
                    state++;
                    StartCoroutine(LoadNewScene());
                }
            }
            //else if (state == 6)
            //{

            //}
        }
        else
        {
            SwipeManager.swipeRight = false;
            SwipeManager.swipeUp = false;
            SwipeManager.swipeDown = false;
            SwipeManager.tap = false;
            SwipeManager.swipeLeft = false;
        }
    }

    public IEnumerator ChangeTransitionState()
    {
        yield return new WaitForSecondsRealtime(transitionTime);
        inTransition = false;
        States[state].SetActive(false);
        States[state + 1].SetActive(true);
        anim.Play(AnimationNames[state + 1]);
        state++;
        // Code to execute after the delay
    }

    public IEnumerator LoadNewScene()
    {
        yield return new WaitForSecondsRealtime(loadSceneTime);
        PlayerPrefs.SetInt("Tutorialdone", 1);
        SceneManager.LoadScene("Game");
    }
}
