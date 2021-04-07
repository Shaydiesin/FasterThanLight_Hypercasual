using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class MenuScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject einstien;
    public Material glow;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBullets", 16f, .01f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SpawnBullets()
    {           
        GameObject go=  Instantiate(bulletPrefab, new Vector3(Random.Range(8, -8), Random.Range(8, -8), Random.Range(-10, -11f)), Quaternion.Euler(Random.Range(0,360), Random.Range(0, 360),Random.Range(0, 360)));
        Destroy(go,10f);
        glow.EnableKeyword("_EMISSION");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
