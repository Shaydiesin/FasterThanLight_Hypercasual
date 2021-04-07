using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public Transform[] Positions;
    public float ObjectSpeed;
    Transform NextPos;
    public bool CanMove;

    int NextPosIndex;

    void Start()
    {
        NextPos = Positions[0];
        CanMove = true;
;   }

    void MovePlayer()
    {
        if(transform.position==NextPos.position)
        {
            NextPosIndex++;
            CanMove = false;
            if (NextPosIndex > (Positions.Length)-1)
                 return;
            NextPos = Positions[NextPosIndex];            
        }

        if (CanMove==true)
        {
            transform.position = Vector3.MoveTowards(transform.position,NextPos.position,ObjectSpeed*Time.deltaTime*1/Time.timeScale);
        }
    }
    void Update()
    {
        MovePlayer();
    }
}
