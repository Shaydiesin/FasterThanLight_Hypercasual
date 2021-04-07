using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //GameObject LevelBounds;
    public GameObject EnemyPrefab;

    float[] lane;
    float end;
    public List<GameObject> Enemies;
    public List<GameObject>[] FrontMaintainer;
    GameObject player;
    public float InitialNumber = 10;
    float SpawnLocation;
    public float InterEnemyDistance = 5.0f;
    public float DespawnDistance = 5.0f;
    // Start is called before the first frame update
    void Awake()
    {
        FrontMaintainer = new List<GameObject>[3];
        for (int i=0; i<3; i++)
        {
            FrontMaintainer[i] = new List<GameObject>();
        }
        lane = new float[3];
        lane[0] = transform.GetChild(0).transform.position.x;
        lane[1] = transform.GetChild(1).transform.position.x;
        lane[2] = transform.GetChild(2).transform.position.x;
        SpawnLocation = transform.GetChild(3).transform.position.z;
        end = transform.GetChild(4).transform.position.z;
        
        Enemies = new List<GameObject>();
        player = GameObject.Find("player");       
    }

    private void Start()
    {
        for (int i = 0; i < InitialNumber; i++)
        {
            spawn_enemy();
        }
        for (int i = 0; i < 3; i++)
        {
            if (FrontMaintainer[i].Count > 0)
            {
                FrontMaintainer[i][0].GetComponent<EnemyScript>().delayActivate = true;
                StartCoroutine(FrontMaintainer[i][0].GetComponent<EnemyScript>().ActivateAfterDelay());
            }
        }
    }
    private void spawn_enemy()
    {
        //Debug.Log(SpawnLocation - player.transform.position.z);
        GameObject new_enemy = Instantiate(EnemyPrefab);
        int selectedLane = Random.Range(0, 3);
        new_enemy.transform.position = new Vector3 (lane[selectedLane], transform.position.y, SpawnLocation);
        Enemies.Add(new_enemy);
        FrontMaintainer[selectedLane].Add(new_enemy);
        SpawnLocation += InterEnemyDistance;
    }

    // Update is called once per frame
    void Update()
    {

        if (Enemies.Count > 0)
        {
            if (Enemies[0]!=null)
            {
                if (Enemies[0].transform.position.z < player.transform.position.z - DespawnDistance)
                {
                    Debug.Log("Entered");
                    GameObject enemy = Enemies[0];
                    for (int i=0; i<3; i++)
                    {
                        if (FrontMaintainer[i].Count > 0)
                        { 
                            if (Enemies[0] == FrontMaintainer[i][0])
                            {
                                FrontMaintainer[i].RemoveAt(0);
                            }
                        }
                    }
                    Enemies.RemoveAt(0);
                    {
                        Destroy(enemy);
                        spawn_enemy();
                    }
                }
            }
            else
            {
                Debug.Log("Entered");
                GameObject enemy = Enemies[0];
                for (int i = 0; i < 3; i++)
                {
                    if (FrontMaintainer[i].Count > 0)
                    {
                        if (Enemies[0] == FrontMaintainer[i][0])
                        {
                            FrontMaintainer[i].RemoveAt(0);
                        }
                    }
                }
                Enemies.RemoveAt(0);
                {
                    Destroy(enemy);
                    spawn_enemy();
                }
            }
        }

        for (int i=0; i<3; i++)
        {
            if (FrontMaintainer[i].Count > 0)
            {
                if (FrontMaintainer[i][0].GetComponent<EnemyScript>().delayActivate == false && FrontMaintainer[i][0].GetComponent<EnemyScript>().stoppedShooting == false)
                    FrontMaintainer[i][0].GetComponent<Animator>().SetBool("transition", true);
            }
        }
    }
}
