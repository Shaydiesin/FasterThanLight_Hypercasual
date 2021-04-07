using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SuperHotScript : MonoBehaviour
{

    public static SuperHotScript instance;

    public float charge;
    public bool canShoot = true;
    public bool action;
    public GameObject bullet;
    public Transform bulletSpawner;
    public float ThrowTime = 0.2f;


    [Header("Weapon")]
    public WeaponScript weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;


    [Space]
    [Header("UI")]
    public Image indicator;
   
    [Space]
    [Header("Prefabs")]
    public GameObject hitParticlePrefab;
    public GameObject bulletPrefab;
    public GameObject deathScreen;

    public int lifeCount;

    public int enemiesKilled=0;
    private void Awake()
    {   
        // set instance to this script
        instance = this;
        // get reference of weapon script from weapon holder on player
        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
            weapon = weaponHolder.GetComponentInChildren<WeaponScript>();
        // number of lives of the player
        lifeCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemiesKilled);
        // make animation timescale independent
        GameObject.Find("WeaponPlaceHolder").GetComponent<Animator>().speed = 0.5f / Time.timeScale;
    }

    // coroutine to quickly change to normal speed and back if action is happening
    public IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(.06f);
        action = false;
    }

    // function to implement player death
    public void PlayerDeath()
    {
        // reduce life
        lifeCount--;
        // if no life left
        if (lifeCount == 0)
        {
            // make player fall to gorund
            GameObject.Find("player").GetComponent<Rigidbody>().useGravity=true;
            // play the death animation
            GameObject.Find("Running").GetComponent<Animator>().SetBool("Death",true);
            
            // disable controller and slo motion time management
            GetComponent<EndlessRunnerController>().enabled = false;
            GetComponent<FTLMeter>().enabled = false;
            Time.timeScale = 1;

            // reset the scene
            Invoke("ReloadScene", 2f * Time.timeScale);

        }
    }

    // function that reloads the current scene
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    // function that loads the main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // if player hits an enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KatanaDetector")
        {
            // if player has weapon, throw it
            if (weapon != null)
            {
                StopCoroutine(ActionE(.1f));
                StartCoroutine(ActionE(.1f));
                StartCoroutine(ThrowAnim());
                weapon.Throw();
                
                weapon = null;
            }
        }
    }

    // coroutine to play throw animation
    public IEnumerator ThrowAnim()
    {
        GameObject.Find("Running").GetComponent<Animator>().SetBool("Throw", true);
        yield return new WaitForSeconds(ThrowTime);
        Debug.Log("YP");
        GameObject.Find("Running").GetComponent<Animator>().SetBool("Throw", false);
        
    }

}
