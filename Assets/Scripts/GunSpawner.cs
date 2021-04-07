using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject glass;
    public GameObject bottle;
    public GameObject knife;
    public GameObject pencil;
    public GameObject katana;
    public GameObject bowl;
    public GameObject rugby;
    public GameObject torch;
    public GameObject donut;
    public GameObject apple;
    public GameObject hammer;
    public GameObject ff;
    public GameObject boomberang;

    GameObject go;  
    public List<GameObject> weaponsPicked = new List<GameObject>();
    bool hasPicked;
    void Start()
    {
        hasPicked = false;
        int i = Random.Range(1, 14);
        if (i == 1)
            go= Instantiate(glass,transform.position,glass.transform.rotation);
        else if (i == 2)
            go = Instantiate(bottle, transform.position , bottle.transform.rotation);
        else if (i == 3)
            go = Instantiate(knife, transform.position , knife.transform.rotation);
        else if (i == 4)
            go = Instantiate(pencil, transform.position , pencil.transform.rotation);
        else if (i == 5)
            go = Instantiate(katana, transform.position , katana.transform.rotation);
        else if (i == 6)
            go = Instantiate(bowl, transform.position, bowl.transform.rotation);
        else if (i == 7)
            go = Instantiate(rugby, transform.position, rugby.transform.rotation);
        else if (i == 8)
            go = Instantiate(apple, transform.position, apple.transform.rotation);
        else if (i == 9)
            go = Instantiate(torch, transform.position, torch.transform.rotation);
        else if (i == 10)
            go = Instantiate(donut, transform.position, donut.transform.rotation);
        else if (i == 11)
            go = Instantiate(hammer, transform.position, hammer.transform.rotation);
        else if (i == 12)
            go = Instantiate(ff, transform.position, ff.transform.rotation);
        else if (i == 13)
            go = Instantiate(boomberang, transform.position, boomberang.transform.rotation);
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="player")
        {
            if (SuperHotScript.instance.weapon == null)
            {
                var scrSup = GameObject.Find("player").GetComponent<SuperHotScript>();
                StopCoroutine(scrSup.ActionE(.1f));
                StartCoroutine(scrSup.ActionE(.1f));
                go.GetComponent<WeaponScript>().Pickup();
                weaponsPicked.Add(go);
                hasPicked = true;
            }                
            else
            {
                weaponsPicked.Add(go);
                hasPicked = true;
            }
        }
    }

    private void OnDestroy()
    {
        if (hasPicked == false)
            Destroy(go);
    }

    private void Update()
    {
    }
}
