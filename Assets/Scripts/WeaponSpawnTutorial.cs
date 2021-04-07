using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnTutorial : MonoBehaviour
{
    //GameObject LevelBounds;
    public GameObject WeaponPrefab;

    float[] lane;
    float end;
    public List<GameObject> Weapons;
    GameObject player;
    public int InitialNumber = 10;
    float SpawnLocation;
    public float InterWeaponDistance = 5.0f;
    public float DespawnDistance = 5.0f;
    public float MinDistanceToEnemy = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        lane = new float[3];
        lane[0] = transform.GetChild(0).transform.position.x;
        lane[1] = transform.GetChild(1).transform.position.x;
        lane[2] = transform.GetChild(2).transform.position.x;
        SpawnLocation = transform.GetChild(3).transform.position.z;
        end = transform.GetChild(4).transform.position.z;

        Weapons = new List<GameObject>();
        player = GameObject.Find("player");

        for (int i = 0; i < InitialNumber; i++)
        {
            spawn_weapon();
        }
    }

    private void spawn_weapon()
    {
        GameObject new_weapon = Instantiate(WeaponPrefab);
        new_weapon.transform.position = new Vector3(lane[Random.Range(0, 3)], transform.position.y, SpawnLocation); ;
        Weapons.Add(new_weapon);
        SpawnLocation += InterWeaponDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (Weapons.Count > 0)
        {
            if (Weapons[0].transform.position.z < player.transform.position.z - DespawnDistance)
            {
                GameObject weapon = Weapons[0];
                Weapons.RemoveAt(0);
                Destroy(weapon);
                {
                    spawn_weapon();
                }
            }
        }
    }
}
