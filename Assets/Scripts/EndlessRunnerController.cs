using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public enum side { Left,Mid,Right }
public class EndlessRunnerController : MonoBehaviour
{
    public side whichSide = side.Mid;
    float NextXPos = 0f;
    public bool swipeLeft;
    public bool swipeRight;
    public float dodgeDistance;
    public float dodgeSpeed;
    public float forwardSpeed;
    bool isDead = false;
    private CharacterController cc;
    private float x;
    public Animator player_anim;
    public float JumpTime = 1f;
    


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(forwardSpeed);
        swipeLeft = SwipeManager.swipeLeft;
        swipeRight = SwipeManager.swipeRight;

        if (swipeLeft)
        {
            if (whichSide == side.Mid)
            {
                StartCoroutine(Jump());
                NextXPos = -dodgeDistance;
                whichSide = side.Left;
            }
            else if (whichSide == side.Right)
            {
                StartCoroutine(Jump());
                //player_anim.SetBool("Jump_Control", true);
                NextXPos = 0;
                whichSide = side.Mid;
            }
        }
        else if (swipeRight)
        {
            if (whichSide == side.Mid)
            {
                StartCoroutine(Jump());
                //player_anim.SetBool("Jump_Control", true);
                NextXPos = dodgeDistance;
                whichSide = side.Right;
            }
            else if (whichSide == side.Left)
            {
                StartCoroutine(Jump());
                //player_anim.SetBool("Jump_Control", true);
                NextXPos = 0;
                whichSide = side.Mid;
            }
        }
        else
        {
            player_anim.SetBool("Jump_Control", false);
        }

        x = Mathf.Lerp(x, NextXPos, Time.deltaTime * dodgeSpeed * 1 / Time.timeScale);
        Vector3 moveVector = new Vector3(x - transform.position.x, 0, forwardSpeed * Time.deltaTime);
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "death")
        {
            forwardSpeed = 0.0f;
            if (isDead == false)
            {
                isDead = true;
                GetComponent<SuperHotScript>().PlayerDeath();
            }
        }
    }
    private IEnumerator Jump()
    {
        player_anim.SetBool("Jump_Control", true);
        yield return new WaitForSeconds(JumpTime);
        player_anim.SetBool("Jump_Control", false);
    }

    public void AdjustForwardSpeed(float newSpeed)
    {
        forwardSpeed = newSpeed;
    }
}
