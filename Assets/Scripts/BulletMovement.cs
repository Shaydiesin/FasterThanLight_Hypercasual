using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    SuperHotScript scr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scr = GameObject.Find("player").GetComponent<SuperHotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  - Vector3.forward * speed * Time.deltaTime;
        //if (GetComponent<MeshRenderer>().isVisible == false)
        //{
        //}
    }

    private void OnBecameInvisible()
    {
        GetComponent<BoxCollider>().enabled = false;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="player")
        {
            SuperHotScript.instance.PlayerDeath();
        }
    }

}
