using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelProgression : MonoBehaviour
{
    int Level;
    public GameObject levelGoal;
    public GameObject panel;
    float timeCheck=0;
    public TextMeshProUGUI levelIndicator;
    public TextMeshProUGUI goalIndicator;
    public TextMeshProUGUI killIndicator;
    public GameObject player;
    int requiredKills;
    public GameObject enemySpawner;

    private void Awake()
    {
        levelGoal.SetActive(true);
        timeCheck = 0;        
        Level = PlayerPrefs.GetInt("LevelIntijar") + 1;
        // set required kills to the level number
        requiredKills = Level;
        levelIndicator.text = "LEVEL " + Level.ToString();
        goalIndicator.text = "KILL " + Level.ToString() + " ENEMIES";

        //make the game more difficult as level progresses
        if (Level < 20)
            player.GetComponent<EndlessRunnerController>().forwardSpeed += .02f * player.GetComponent<EndlessRunnerController>().forwardSpeed * Level;
        else
            player.GetComponent<EndlessRunnerController>().forwardSpeed = 7.5f;

        if (Level < 12)
            enemySpawner.GetComponent<EnemySpawn>().InterEnemyDistance = 20 - Level;
        else
            enemySpawner.GetComponent<EnemySpawn>().InterEnemyDistance = 8;
    }
    // loade new game scene
   IEnumerator ChangeGameScene()
    {
        yield return new WaitForSecondsRealtime(.5f);
        Level++;
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("LevelIntijar", Level - 1);
    }
    void Update()
    {
        timeCheck += Time.unscaledDeltaTime;
     
        // wait for some time and then remove level goal instruction
        if (timeCheck > 2f)
        {
            levelGoal.SetActive(false);
            panel.SetActive(false);
        }
        levelGoal.GetComponent<Animator>().speed = 1 / Time.timeScale;
        panel.GetComponent<Animator>().speed = 1 / Time.timeScale;
        killIndicator.text = SuperHotScript.instance.enemiesKilled.ToString();

        // if player has killed enough enemies, change scene
        if(SuperHotScript.instance.enemiesKilled==requiredKills)
        {
            StartCoroutine(ChangeGameScene());
        }        
    }
}
